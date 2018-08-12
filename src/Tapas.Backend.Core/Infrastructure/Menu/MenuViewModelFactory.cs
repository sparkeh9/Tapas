namespace Tapas.Backend.Core.Infrastructure.Menu
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Areas.Backend.ViewModels.Menu;
    using ExtCore.Infrastructure;
    using Metadata;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    public class MenuViewModelFactory
    {
        private readonly IAuthorizationService authorisationService;
        private readonly IUrlHelper urlHelper;
        private readonly ClaimsPrincipal principal;

        public MenuViewModelFactory( IUrlHelper urlHelper, IAuthorizationService authorisationService, IActionContextAccessor accessor )
        {
            this.urlHelper = urlHelper;
            this.authorisationService = authorisationService;

            principal = accessor.ActionContext.HttpContext.User;
        }

        public async Task<MenuViewModel> CreateAsync()
        {
            var menuGroupViewModels = new Dictionary<string, MenuGroupViewModel>();
            foreach ( var extensionMetadata in ExtensionManager.GetInstances<IBackendExtensionMetadata>() )
            {
                extensionMetadata.UrlHelper = urlHelper;

                var menuGroups = extensionMetadata.MenuGroups();
                if ( menuGroups == null )
                {
                    continue;
                }

                foreach ( var menuGroup in menuGroups )
                {
                    var menuGroupViewModel = FindOrCreateMenuGroup( menuGroupViewModels, menuGroup );
                    var menuItemViewModels = menuGroupViewModel.MenuItems;

                    if ( menuGroup.MenuItems == null )
                    {
                        continue;
                    }

                    foreach ( var menuItem in menuGroup.MenuItems )
                    {
                        if ( await ShouldAddMenuItemAsync( menuItem ) )
                        {
                            menuItemViewModels.Add( new MenuItemViewModelFactory().Create( menuItem ) );
                        }
                    }

                    menuGroupViewModel.MenuItems = menuItemViewModels.OrderBy( mi => mi.Position )
                                                                     .ToList();
                }
            }

            return new MenuViewModel
            {
                MenuGroups = menuGroupViewModels.Values
                                                .Where( x => x.MenuItems.Any() )
                                                .OrderBy( x => x.Position )
                                                .ThenBy( x => x.Name )
            };
        }

        private async Task<bool> ShouldAddMenuItemAsync( MenuItem menuItem )
        {
            bool matchedAllPolicies;
            bool matchedAnyRole;

            if ( menuItem.AllPolicies.Any() )
            {
                var results = await Task.WhenAll( menuItem.AllPolicies.Select( x => authorisationService.AuthorizeAsync( principal, x ) ) );
                matchedAllPolicies = results.All( x => x.Succeeded );
            }
            else
            {
                matchedAllPolicies = true;
            }
            
            if ( menuItem.AnyRoles.Any() )
            {
                matchedAnyRole = menuItem.AnyRoles.Any( x => principal.HasClaim( Tapas.Core.Security.ClaimTypes.Permission, x ) );
            }
            else
            {
                matchedAnyRole = true;
            }

            return matchedAllPolicies && matchedAnyRole;
        }


        private static MenuGroupViewModel FindOrCreateMenuGroup( IDictionary<string, MenuGroupViewModel> menuGroupViewModels, MenuGroup menuGroup )
        {
            menuGroupViewModels.TryGetValue( menuGroup.Name, out var menuGroupViewModel );

            if ( menuGroupViewModel == null )
            {
                menuGroupViewModel = new MenuGroupViewModelFactory().Create( menuGroup );
                menuGroupViewModels.Add( menuGroupViewModel.Name, menuGroupViewModel );
            }
            else
            {
                if ( menuGroup.Position < menuGroupViewModel.Position )
                {
                    menuGroupViewModel.CssClass = menuGroup.CssClass;
                    menuGroupViewModel.Position = menuGroup.Position;
                }
            }

            return menuGroupViewModel;
        }
    }
}
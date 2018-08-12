namespace Tapas.Backend.UserManagement
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Infrastructure.Menu;
    using Core.Infrastructure.Metadata;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ExtensionMetadata : IBackendExtensionMetadata
    {
        private readonly IUrlHelper urlHelper;

        public ExtensionMetadata( IUrlHelper urlHelper )
        {
            this.urlHelper = urlHelper;
        }

        public IEnumerable<StyleSheet> StyleSheets() => Enumerable.Empty<StyleSheet>();

        public IEnumerable<Script> Scripts() => Enumerable.Empty<Script>();

        public IEnumerable<MenuGroup> MenuGroups()
        {
            yield return new MenuGroup( "Administration", 0, new List<MenuItem>
            {
                new MenuItem( "ManageUsers", "Manage Users", 999, urlHelper.Action( "Index", "Users", new { area = "Backend" } ), "fa fa-users", new List<AuthorizeAttribute>
                {
                    new AuthorizeAttribute( "Backend:AccessBackend" ),
                    new AuthorizeAttribute( "Backend:Users:Manage" ),
                } )
            } );
        }
    }
}
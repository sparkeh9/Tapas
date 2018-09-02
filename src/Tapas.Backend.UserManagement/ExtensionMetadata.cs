namespace Tapas.Backend.UserManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Infrastructure.Menu;
    using Core.Infrastructure.Metadata;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ExtensionMetadata : IBackendExtensionMetadata
    {
        public IUrlHelper UrlHelper { get; set; }

        public IEnumerable<BackendStyleSheet> StyleSheets()
        {
            return Enumerable.Empty<BackendStyleSheet>();
        }

        public IEnumerable<BackendScript> Scripts()
        {
            yield return new BackendScript("~/wwwroot.backend/js/jquery.auto-complete.min.js", 999);
        }

        public IEnumerable<MenuGroup> MenuGroups()
        {
            if ( UrlHelper == null )
            {
                throw new ArgumentNullException( nameof( UrlHelper ), "UrlHelper must be provided" );
            }

            yield return new MenuGroup( "Administration", "Administration", 0, new List<MenuItem>
            {
                new MenuItem( "ManageUsers", "Manage Users", 1, UrlHelper.Action( "Index", "Users", new { area = "Backend" } ), "fa fa-users", new List<AuthorizeAttribute>
                {
                    new AuthorizeAttribute( "Backend:Users:Manage" )
                } ),
                new MenuItem( "ManageRoles", "Manage Roles", 2, UrlHelper.Action( "Index", "Roles", new { area = "Backend" } ), "fa fa-unlock", new List<AuthorizeAttribute>
                {
                    new AuthorizeAttribute( "Backend:Users:Manage" )
                } )
            },"fa fa-lock" );
        }
    }
}
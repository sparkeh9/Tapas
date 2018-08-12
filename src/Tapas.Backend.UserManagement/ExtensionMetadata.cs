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

        public IEnumerable<StyleSheet> StyleSheets()
        {
            return Enumerable.Empty<StyleSheet>();
        }

        public IEnumerable<Script> Scripts()
        {
            return Enumerable.Empty<Script>();
        }

        public IEnumerable<MenuGroup> MenuGroups()
        {
            if ( UrlHelper == null )
            {
                throw new ArgumentNullException( nameof( UrlHelper ), "UrlHelper must be provided" );
            }

            yield return new MenuGroup( "Administration", "Administration", 0, new List<MenuItem>
            {
                new MenuItem( "ManageUsers", "Manage Users", 999, UrlHelper.Action( "Index", "Users", new { area = "Backend" } ), "fa fa-users", new List<AuthorizeAttribute>
                {
                    new AuthorizeAttribute( "Backend:Users:Manage" )
                } )
            },"fa fa-lock" );
        }
    }
}
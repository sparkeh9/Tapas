﻿namespace Tapas.Backend.UserManagement
{
    using System;
    using System.Collections.Generic;
    using Core.Infrastructure.Menu;
    using Core.Infrastructure.Metadata;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BackendExtensionMetadata : IBackendExtensionMetadata
    {
        public IUrlHelper UrlHelper { get; set; }

        public IEnumerable<BackendStyleSheet> StyleSheets()
        {
            yield return new BackendStyleSheet("~/backend/css/jquery.auto-complete.css", 999 );
        }

        public IEnumerable<BackendScript> Scripts()
        {
            yield return new BackendScript("~/backend/js/jquery.auto-complete.min.js", 999);
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
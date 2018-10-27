namespace Tapas.Cms.FlatFile.Backend
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Tapas.Backend.Core.Infrastructure.Menu;
    using Tapas.Backend.Core.Infrastructure.Metadata;

    public class BackendExtensionMetadata : IBackendExtensionMetadata
    {
        public IUrlHelper UrlHelper { get; set; }

        public IEnumerable<BackendStyleSheet> StyleSheets()
        {
            return Enumerable.Empty<BackendStyleSheet>();
//            yield return new BackendStyleSheet("~/wwwroot.backend/css/jquery.auto-complete.css", 999 );
        }

        public IEnumerable<BackendScript> Scripts()
        {
            return Enumerable.Empty<BackendScript>();
//            yield return new BackendScript("~/wwwroot.backend/js/jquery.auto-complete.min.js", 999);
        }

        public IEnumerable<MenuGroup> MenuGroups()
        {
            if ( UrlHelper == null )
            {
                throw new ArgumentNullException( nameof( UrlHelper ), "UrlHelper must be provided" );
            }

            yield return new MenuGroup( "Administration", "Administration", 0, new List<MenuItem>
            {
                new MenuItem( "ManagePages", "Manage Pages", 1, UrlHelper.Action( "Index", "Pages", new { area = "Backend" } ), "fa fa-users", new List<AuthorizeAttribute>
                {
                    new AuthorizeAttribute( "Backend:Pages:Manage" )
                } )
            },"fa fa-lock" );
        }
    }
}
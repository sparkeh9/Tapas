namespace Tapas.Backend.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure.Menu;
    using Infrastructure.Metadata;
    using Microsoft.AspNetCore.Mvc;

    public class ExtensionMetadata : IBackendExtensionMetadata
    {
        public IUrlHelper UrlHelper { get; set; }

        public IEnumerable<BackendStyleSheet> StyleSheets()
        {
            yield return new BackendStyleSheet("~/wwwroot.backend/css/vendor.css", 1 );
            yield return new BackendStyleSheet("~/wwwroot.backend/css/app-orange.css", 2 );
        }

        public IEnumerable<BackendScript> Scripts()
        {
            yield return new BackendScript("~/wwwroot.backend/js/vendor.js", 1);
            yield return new BackendScript("~/wwwroot.backend/js/app.js", 2);
        }

        public IEnumerable<MenuGroup> MenuGroups()
        {
            if ( UrlHelper == null )
            {
                throw new ArgumentNullException( nameof( UrlHelper ), "UrlHelper must be provided" );
            }

            return Enumerable.Empty<MenuGroup>();
        }
    }
}
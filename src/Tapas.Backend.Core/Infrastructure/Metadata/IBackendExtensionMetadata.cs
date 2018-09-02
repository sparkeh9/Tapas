namespace Tapas.Backend.Core.Infrastructure.Metadata
{
    using System.Collections.Generic;
    using Menu;
    using Microsoft.AspNetCore.Mvc;

    public interface IBackendExtensionMetadata
    {
        IUrlHelper UrlHelper { get; set; }
        IEnumerable<BackendStyleSheet> StyleSheets();
        IEnumerable<BackendScript> Scripts();
        IEnumerable<MenuGroup> MenuGroups();
    }
}
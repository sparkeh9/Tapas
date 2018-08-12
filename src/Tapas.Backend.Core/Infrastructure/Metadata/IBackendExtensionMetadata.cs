namespace Tapas.Backend.Core.Infrastructure.Metadata
{
    using System.Collections.Generic;
    using Menu;
    using Microsoft.AspNetCore.Mvc;

    public interface IBackendExtensionMetadata
    {
        IUrlHelper UrlHelper { get; set; }
        IEnumerable<StyleSheet> StyleSheets();
        IEnumerable<Script> Scripts();
        IEnumerable<MenuGroup> MenuGroups();
    }
}
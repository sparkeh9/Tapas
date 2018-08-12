namespace Tapas.Backend.Core.Infrastructure.Metadata
{
    using System.Collections.Generic;
    using Menu;

    public interface IBackendExtensionMetadata
    {
        IEnumerable<StyleSheet> StyleSheets();
        IEnumerable<Script> Scripts();
        IEnumerable<MenuGroup> MenuGroups();
    }
}
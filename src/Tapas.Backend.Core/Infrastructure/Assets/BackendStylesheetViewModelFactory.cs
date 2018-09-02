namespace Tapas.Backend.Core.Infrastructure.Assets
{
    using System.Collections.Generic;
    using System.Linq;
    using ExtCore.Infrastructure;
    using Metadata;

    public class BackendStylesheetViewModelFactory
    {
        public BackendStyleSheetViewModel Create()
        {
            var scripts = new List<BackendStyleSheet>();

            foreach ( var extensionMetadata in ExtensionManager.GetInstances<IBackendExtensionMetadata>() )
            {
                scripts.AddRange( extensionMetadata.StyleSheets() );
            }

            return new BackendStyleSheetViewModel
            {
                Stylesheets = scripts.OrderBy( s => s.Position )
            };
        }
    }
}
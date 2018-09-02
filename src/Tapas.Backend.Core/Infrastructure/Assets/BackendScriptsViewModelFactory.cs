namespace Tapas.Backend.Core.Infrastructure.Assets
{
    using System.Collections.Generic;
    using System.Linq;
    using ExtCore.Infrastructure;
    using Metadata;

    public class BackendScriptsViewModelFactory
    {
        public BackendScriptViewModel Create()
        {
            var scripts = new List<BackendScript>();

            foreach ( var extensionMetadata in ExtensionManager.GetInstances<IBackendExtensionMetadata>() )
            {
                scripts.AddRange( extensionMetadata.Scripts() );
            }

            return new BackendScriptViewModel
            {
                Scripts = scripts.OrderBy( s => s.Position )
            };
        }
    }
}
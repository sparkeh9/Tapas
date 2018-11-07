namespace Tapas.Cms.FlatFile.Backend.Actions
{
    using System;
    using System.Reflection;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.Extensions.DependencyInjection;
    using Tapas.Core.ExtensionMethods;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
#if DEBUG
            string extensionPath = TapasDebugExtensions.GetExtensionPath( Assembly.GetExecutingAssembly().Location, "../../../../", typeof( BackendExtensionMetadata ).Namespace );
            serviceCollection.AddTapasDebugRazorFileProvider( extensionPath );
#endif
        }
    }
}
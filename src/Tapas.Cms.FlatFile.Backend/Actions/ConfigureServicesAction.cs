namespace Tapas.Cms.FlatFile.Backend.Actions
{
    using System;
    using System.Reflection;
    using Core.ExtensionMethods;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

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
namespace Tapas.Core.ExtensionMethods
{
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;

    public static class TapasDebugExtensions
    {
        public static string GetExtensionPath( string assemblyLocation, string relativeNavigation, string nameSpace )
        {
            return Path.GetFullPath( Path.Combine( Path.GetDirectoryName( assemblyLocation ), $"{relativeNavigation}/{nameSpace}" ) );
        }

        public static IServiceCollection AddTapasDebugRazorFileProvider( this IServiceCollection serviceCollection, string filePath )
        {
            serviceCollection.Configure<RazorViewEngineOptions>( options => { options.FileProviders.Add( new PhysicalFileProvider( filePath ) ); } );
            return serviceCollection;
        }
    }
}
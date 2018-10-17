namespace Tapas.Cms.FlatFile.Core.Actions
{
    using System;
    using ExtCore.Infrastructure.Actions;
    using Git;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var flatFileCmsOptions = serviceProvider.GetService<IOptions<FlatFileCmsGitOptions>>()?.Value;

            var fileProvider = new PhysicalFileProvider(flatFileCmsOptions.FilePath);
            serviceCollection.AddSingleton(fileProvider);
            serviceCollection.AddTransient<FlatFileCmsProviderRazorProjectFileSystem>();
            serviceCollection.AddSingleton<IPageRouteModelProvider, FlatFileCmsRazorProjectPageRouteModelProvider>();
            serviceCollection.AddTransient<IGitRepositoryProvider, GitRepositoryProvider>();

            var mvcBuilder = serviceProvider.GetService<IMvcBuilder>();

//            if ( flatFileCmsOptions == null || flatFileCmsOptions.FilePath.IsNullOrWhiteSpace() )
//            {
//                throw new ArgumentNullException( $"{nameof( FlatFileCmsGitOptions )}.{nameof( FlatFileCmsGitOptions.FilePath )}" );
//            }

//            var relativePath = MakeRelative( flatFileCmsOptions.FilePath, AppDomain.CurrentDomain.BaseDirectory );

            mvcBuilder.AddRazorPagesOptions( options => { options.AllowAreas = true; } );
        }

//        public static string MakeRelative( string filePath, string referencePath )
//        {
//            var fileUri = new Uri( filePath );
//            var referenceUri = new Uri( referencePath );
//            return referenceUri.MakeRelativeUri( fileUri ).ToString();
//        }
    }
}
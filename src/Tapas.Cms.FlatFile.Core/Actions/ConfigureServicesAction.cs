namespace Tapas.Cms.FlatFile.Core.Actions
{
    using System;
    using ExtCore.Infrastructure.Actions;
    using Git;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Tapas.Core.ExtensionMethods;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            serviceCollection.AddTransient<IGitRepositoryProvider, GitRepositoryProvider>();

            var flatFileCmsOptions = serviceProvider.GetService<IOptions<FlatFileCmsGitOptions>>()?.Value;
            var mvcBuilder = serviceProvider.GetService<IMvcBuilder>();

            if ( flatFileCmsOptions == null || flatFileCmsOptions.FilePath.IsNullOrWhiteSpace() )
            {
                throw new ArgumentNullException( $"{nameof( FlatFileCmsGitOptions )}.{nameof( FlatFileCmsGitOptions.FilePath )}" );
            }

            var relativePath = MakeRelative( flatFileCmsOptions.FilePath, AppDomain.CurrentDomain.BaseDirectory );

            mvcBuilder.AddRazorPagesOptions( options =>
                                             {
                                                 options.AllowAreas = true;
//                                                 options.RootDirectory = flatFileCmsOptions.FilePath;
                                             } );
        }

        public static string MakeRelative( string filePath, string referencePath )
        {
            var fileUri = new Uri( filePath );
            var referenceUri = new Uri( referencePath );
            return referenceUri.MakeRelativeUri( fileUri ).ToString();
        }
    }
}
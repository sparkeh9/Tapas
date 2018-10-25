namespace Tapas.Cms.FlatFile.Core.Actions
{
    using System;
    using System.IO;
    using ExtCore.Infrastructure.Actions;
    using Git;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            serviceCollection.AddTransient<IGitRepositoryProvider, GitRepositoryProvider>();
            serviceCollection.Configure<FlatFileCmsGitOptions>( configuration.GetSection( "Tapas:FlatFileCmsGit" ) );

            var cmsOptions = new FlatFileCmsGitOptions();
            configuration.Bind( "Tapas:FlatFileCmsGit", cmsOptions );

            serviceCollection.AddMvc()
                             .AddRazorPagesOptions( o => { o.AllowAreas = true; } )
                             .AddRazorOptions( options =>
                                               {
                                                   if ( !Directory.Exists( cmsOptions.FilePath ) )
                                                   {
                                                       Directory.CreateDirectory( cmsOptions.FilePath );
                                                   }
                                                   
                                                   options.FileProviders.Add( new PhysicalFileProvider( cmsOptions.FilePath ) );
                                               } );
        }
    }
}
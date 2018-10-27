namespace Tapas.Cms.FlatFile.Core.Actions
{
    using System;
    using System.IO;
    using ExtCore.Infrastructure.Actions;
    using Git;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Tapas.Core.ExtensionMethods;

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

            var repoProvider = serviceCollection.BuildServiceProvider()
                                                .GetRequiredService<IGitRepositoryProvider>();

            serviceCollection.AddMvc()
                             .AddRazorPagesOptions( o => { o.AllowAreas = true; } )
                             .AddRazorOptions( options =>
                                               {
                                                   if ( !IsValidPath( cmsOptions.FilePath ) )
                                                   {
                                                       return;
                                                   }

                                                   if ( !Directory.Exists( cmsOptions.FilePath ) )
                                                   {
                                                       if ( cmsOptions.RepositoryUrl.IsNullOrWhiteSpace() )
                                                       {
                                                           Directory.CreateDirectory( cmsOptions.FilePath );
                                                       }
                                                       else
                                                       {
                                                           repoProvider.CloneRepositoryToPathAsync( cmsOptions );
                                                       }
                                                   }

                                                   options.FileProviders.Add( new PhysicalFileProvider( cmsOptions.FilePath ) );
                                               } );
        }

        private bool IsValidPath(string path, bool exactPath = true)
        {
            bool isValid = true;

            try
            {
                string fullPath = Path.GetFullPath(path);

                if (exactPath)
                {
                    string root = Path.GetPathRoot(path);
                    isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
                }
                else
                {
                    isValid = Path.IsPathRooted(path);
                }
            }
            catch(Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
namespace Tapas.Cms.FlatFile.Core.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Razor.Internal;
    using Microsoft.AspNetCore.Razor.Language;
    using Microsoft.Extensions.FileProviders;

    public class FlatFileCmsProviderRazorProjectFileSystem : RazorProjectFileSystem
    {
        private const string RazorFileExtension = ".cshtml";
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileProvider _provider;

        public FlatFileCmsProviderRazorProjectFileSystem( PhysicalFileProvider fileProvider, IHostingEnvironment hostingEnviroment )
        {
            _provider = fileProvider ?? throw new ArgumentNullException( nameof( fileProvider ) );
            _hostingEnvironment = hostingEnviroment ?? throw new ArgumentNullException( nameof( hostingEnviroment ) );
        }

        public override RazorProjectItem GetItem( string path )
        {
//            path = NormalizeAndEnsureValidPath( path );
            return new FileProviderRazorProjectItem( _provider.GetFileInfo( path ), string.Empty, path, _hostingEnvironment.ContentRootPath );
        }

        public override IEnumerable<RazorProjectItem> EnumerateItems( string path )
        {
//            path = NormalizeAndEnsureValidPath( path );
            var directoryContents = _provider.GetDirectoryContents( path );
            return EnumerateFiles( directoryContents, path, string.Empty );
        }

        private IEnumerable<RazorProjectItem> EnumerateFiles( IDirectoryContents directory, string basePath, string prefix )
        {
            if ( directory.Exists )
            {
                foreach ( var fileInfo in directory )
                {
                    if ( fileInfo.IsDirectory )
                    {
                        string str = prefix + "/" + fileInfo.Name;
                        foreach ( var enumerateFile in EnumerateFiles( _provider.GetDirectoryContents( JoinPath( basePath, str ) ), basePath, str ) )
                        {
                            yield return enumerateFile;
                        }
                    }
                    else if ( string.Equals( ".cshtml", Path.GetExtension( fileInfo.Name ), StringComparison.OrdinalIgnoreCase ) )
                    {
                        string filePath = prefix + "/" + fileInfo.Name;
                        yield return new FileProviderRazorProjectItem( fileInfo, basePath, filePath, _hostingEnvironment.ContentRootPath );
                    }
                }
            }
        }

        private static string JoinPath( string path1, string path2 )
        {
            bool flag1 = path1.EndsWith( "/", StringComparison.Ordinal );
            bool flag2 = path2.StartsWith( "/", StringComparison.Ordinal );
            if ( flag2 & flag1 )
            {
                return path1 + path2.Substring( 1 );
            }

            if ( flag2 | flag1 )
            {
                return path1 + path2;
            }

            return path1 + "/" + path2;
        }
    }
}
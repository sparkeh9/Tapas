namespace Tapas.Cms.FlatFile.Core.Infrastructure
{
    using System;
    using System.IO;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.FileProviders.Internal;
    using Microsoft.Extensions.FileProviders.Physical;
    using Microsoft.Extensions.Primitives;

    public class PhysicalFileProvider : IFileProvider, IDisposable
    {
        private const string PollingEnvironmentKey = "DOTNET_USE_POLLING_FILE_WATCHER";
        private readonly PhysicalFilesWatcher _filesWatcher;
        private readonly ExclusionFilters _filters;

        /// <summary>The root directory for this instance.</summary>
        public string Root { get; }

        private static readonly char[] _pathSeparators = new char[ 2 ]
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        /// <summary>
        ///     Initializes a new instance of a PhysicalFileProvider at the given root directory.
        /// </summary>
        /// <param name="root">The root directory. This should be an absolute path.</param>
        public PhysicalFileProvider( string root ) : this( root, CreateFileWatcher( root, ExclusionFilters.Sensitive ), ExclusionFilters.Sensitive ) { }

        /// <summary>
        ///     Initializes a new instance of a PhysicalFileProvider at the given root directory.
        /// </summary>
        /// <param name="root">The root directory. This should be an absolute path.</param>
        /// <param name="filters">Specifies which files or directories are excluded.</param>
        public PhysicalFileProvider( string root, ExclusionFilters filters ) : this( root, CreateFileWatcher( root, filters ), filters ) { }

        internal PhysicalFileProvider( string root, PhysicalFilesWatcher physicalFilesWatcher ) : this( root, physicalFilesWatcher, ExclusionFilters.Sensitive ) { }

        private PhysicalFileProvider( string root, PhysicalFilesWatcher physicalFilesWatcher, ExclusionFilters filters )
        {
            if ( !Path.IsPathRooted( root ) )
            {
                throw new ArgumentException( "The path must be absolute.", nameof( root ) );
            }

            Root = PathUtils.EnsureTrailingSlash( Path.GetFullPath( root ) );
            if ( !Directory.Exists( Root ) )
            {
                throw new DirectoryNotFoundException( Root );
            }

            _filesWatcher = physicalFilesWatcher;
            _filters = filters;
        }

        /// <summary>
        ///     Disposes the provider. Change tokens may not trigger after the provider is disposed.
        /// </summary>
        public void Dispose()
        {
            _filesWatcher.Dispose();
        }

        /// <summary>
        ///     Locate a file at the given path by directly mapping path segments to physical directories.
        /// </summary>
        /// <param name="subpath">A path under the root directory</param>
        /// <returns>
        ///     The file information. Caller must check <see cref="P:Microsoft.Extensions.FileProviders.IFileInfo.Exists" />
        ///     property.
        /// </returns>
        public IFileInfo GetFileInfo( string subpath )
        {
            if ( string.IsNullOrEmpty( subpath ) || PathUtils.HasInvalidPathChars( subpath ) )
            {
                return new NotFoundFileInfo( subpath );
            }

            subpath = subpath.TrimStart( _pathSeparators );
            if ( Path.IsPathRooted( subpath ) )
            {
                return new NotFoundFileInfo( subpath );
            }

            string fullPath = GetFullPath( subpath );
            if ( fullPath == null )
            {
                return new NotFoundFileInfo( subpath );
            }

            var info = new FileInfo( fullPath );
            if ( FileSystemInfoHelper.IsExcluded( info, _filters ) )
            {
                return new NotFoundFileInfo( subpath );
            }

            return new PhysicalFileInfo( info );
        }

        public IDirectoryContents GetDirectoryContents( string fullPath )
        {
            try
            {
//                if ( subpath == null || PathUtils.HasInvalidPathChars( subpath ) )
//                {
//                    return NotFoundDirectoryContents.Singleton;
//                }
//
//                subpath = subpath.TrimStart( _pathSeparators );
//                if ( Path.IsPathRooted( subpath ) )
//                {
//                    return NotFoundDirectoryContents.Singleton;
//                }

//                string fullPath = GetFullPath( subpath );
                if ( fullPath == null || !Directory.Exists( fullPath ) )
                {
                    return NotFoundDirectoryContents.Singleton;
                }

                return new PhysicalDirectoryContents( fullPath, _filters );
            }
            catch ( DirectoryNotFoundException ex ) { }
            catch ( IOException ex ) { }

            return NotFoundDirectoryContents.Singleton;
        }

        public IChangeToken Watch( string filter )
        {
            if ( filter == null || PathUtils.HasInvalidFilterChars( filter ) )
            {
                return NullChangeToken.Singleton;
            }

            filter = filter.TrimStart( _pathSeparators );
            return _filesWatcher.CreateFileChangeToken( filter );
        }

        private static PhysicalFilesWatcher CreateFileWatcher( string root, ExclusionFilters filters )
        {
            string environmentVariable = Environment.GetEnvironmentVariable( "DOTNET_USE_POLLING_FILE_WATCHER" );
            bool pollForChanges = string.Equals( environmentVariable, "1", StringComparison.Ordinal ) || string.Equals( environmentVariable, "true", StringComparison.OrdinalIgnoreCase );
            root = PathUtils.EnsureTrailingSlash( Path.GetFullPath( root ) );
            return new PhysicalFilesWatcher( root, new FileSystemWatcher( root ), pollForChanges, filters );
        }

        private string GetFullPath( string path )
        {
//            if ( PathUtils.PathNavigatesAboveRoot( path ) )
//            {
//                return null;
//            }

            string fullPath;
            try
            {
                fullPath = Path.GetFullPath( Path.Combine( Root, path ) );
            }
            catch
            {
                return null;
            }
//
//            if ( !IsUnderneathRoot( fullPath ) )
//            {
//                return null;
//            }

            return fullPath;
        }
//
//        private bool IsUnderneathRoot( string fullPath )
//        {
//            return fullPath.StartsWith( Root, StringComparison.OrdinalIgnoreCase );
//        }
    }
}
namespace Tapas.Cms.FlatFile.Core.Tests.Infrastructure.Helpers
{
    using System.IO;

    internal static class StringExtensions
    {
        public static void DeleteDirectory( this string directoryPath )
        {
            if ( !Directory.Exists( directoryPath ) )
            {
                return;
            }

            var files = Directory.GetFiles( directoryPath );
            var directories = Directory.GetDirectories( directoryPath );

            foreach ( string file in files )
            {
                File.SetAttributes( file, FileAttributes.Normal );
                File.Delete( file );
            }

            foreach ( string dir in directories )
            {
                dir.DeleteDirectory();
            }

            File.SetAttributes( directoryPath, FileAttributes.Normal );
            Directory.Delete( directoryPath, false );
        }
    }
}
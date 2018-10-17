namespace Tapas.Cms.FlatFile.Core.Infrastructure
{
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Primitives;

    internal static class PathUtils
    {
        private static readonly char[] _invalidFileNameChars = Path.GetInvalidFileNameChars().Where( c =>
                                                                                                     {
                                                                                                         if ( c != Path.DirectorySeparatorChar )
                                                                                                         {
                                                                                                             return c != Path.AltDirectorySeparatorChar;
                                                                                                         }

                                                                                                         return false;
                                                                                                     } ).ToArray();

        private static readonly char[] _invalidFilterChars = _invalidFileNameChars.Where( c =>
                                                                                          {
                                                                                              if ( c != '*' && c != '|' )
                                                                                              {
                                                                                                  return c != '?';
                                                                                              }

                                                                                              return false;
                                                                                          } ).ToArray();

        private static readonly char[] _pathSeparators =
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        internal static bool HasInvalidPathChars( string path )
        {
            return path.IndexOfAny( _invalidFileNameChars ) != -1;
        }

        internal static bool HasInvalidFilterChars( string path )
        {
            return path.IndexOfAny( _invalidFilterChars ) != -1;
        }

        internal static string EnsureTrailingSlash( string path )
        {
            if ( !string.IsNullOrEmpty( path ) && path[ path.Length - 1 ] != Path.DirectorySeparatorChar )
            {
                return path + Path.DirectorySeparatorChar;
            }

            return path;
        }

        internal static bool PathNavigatesAboveRoot( string path )
        {
            var stringTokenizer = new StringTokenizer( path, _pathSeparators );
            int num = 0;
            foreach ( var stringSegment in stringTokenizer )
            {
                if ( !stringSegment.Equals( "." ) && !stringSegment.Equals( "" ) )
                {
                    if ( stringSegment.Equals( ".." ) )
                    {
                        --num;
                        if ( num == -1 )
                        {
                            return true;
                        }
                    }
                    else
                    {
                        ++num;
                    }
                }
            }

            return false;
        }
    }
}
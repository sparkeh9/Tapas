namespace Tapas.Cms.FlatFile.Core.Infrastructure
{
    using System;
    using Microsoft.Extensions.Logging;

    internal static class PageLoggerExtensions
    {
        private static readonly Action<ILogger, string, Exception> _unsupportedAreaPath;

        static PageLoggerExtensions()
        {
            _unsupportedAreaPath = LoggerMessage.Define<string>(
                                                                LogLevel.Warning,
                                                                1,
                                                                "The page at '{FilePath}' is located under the area root directory '/Areas/' but does not follow the path format '/Areas/AreaName/Pages/Directory/FileName.cshtml" );
        }

        public static void UnsupportedAreaPath( this ILogger logger, string filePath )
        {
            if ( logger.IsEnabled( LogLevel.Warning ) )
            {
                _unsupportedAreaPath( logger, filePath, null );
            }
        }
    }
}
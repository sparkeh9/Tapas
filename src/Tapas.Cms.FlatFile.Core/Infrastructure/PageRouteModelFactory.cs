namespace Tapas.Cms.FlatFile.Core.Infrastructure
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;

    internal class PageRouteModelFactory
    {
        private readonly ILogger _logger;
        private readonly string _normalizedAreaRootDirectory;
        private readonly string _normalizedRootDirectory;
        private readonly RazorPagesOptions _options;
        private static readonly string IndexFileName = "Index" + RazorViewEngine.ViewExtension;

        public PageRouteModelFactory( RazorPagesOptions options, string rootDirectory, string areaRootDirectory, ILogger logger )
        {
            var razorPagesOptions = options;
            _options = razorPagesOptions ?? throw new ArgumentNullException( nameof( options ) );
            _logger = logger ?? throw new ArgumentNullException( nameof( logger ) );

            _normalizedRootDirectory = rootDirectory.Replace( Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar );
            _normalizedAreaRootDirectory = areaRootDirectory.Replace( Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar );
        }

        public PageRouteModel CreateRouteModel( string relativePath, string routeTemplate )
        {
            string razorFilePath = relativePath.Replace( Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar );
            string viewEnginePath = GetViewEnginePath( _normalizedRootDirectory, razorFilePath );
            string relativeToAppRoot = MakeRelative( razorFilePath, AppDomain.CurrentDomain.BaseDirectory );


            var model = new PageRouteModel( razorFilePath, viewEnginePath );
            PopulateRouteModel( model, viewEnginePath, routeTemplate );
            return model;
        }

        public static string MakeRelative( string filePath, string referencePath )
        {
            var fileUri = new Uri( filePath );
            var referenceUri = new Uri( referencePath );
            return referenceUri.MakeRelativeUri( fileUri ).ToString();
        }


        public PageRouteModel CreateAreaRouteModel( string relativePath, string routeTemplate )
        {
            ValueTuple<string, string> result;
            if ( !TryParseAreaPath( relativePath, out result ) )
            {
                return null;
            }

            var model = new PageRouteModel( relativePath, result.Item2, result.Item1 );
            PopulateRouteModel( model, CreateAreaRoute( result.Item1, result.Item2 ), routeTemplate );
            model.RouteValues[ "area" ] = result.Item1;
            return model;
        }

        private static void PopulateRouteModel( PageRouteModel model, string pageRoute, string routeTemplate )
        {
            model.RouteValues.Add( "page", model.ViewEnginePath );
            var selectorModel = CreateSelectorModel( pageRoute, routeTemplate );
            model.Selectors.Add( selectorModel );
            string fileName = Path.GetFileName( model.RelativePath );
            if ( AttributeRouteModel.IsOverridePattern( routeTemplate ) || !string.Equals( IndexFileName, fileName, StringComparison.OrdinalIgnoreCase ) )
            {
                return;
            }

            selectorModel.AttributeRouteModel.SuppressLinkGeneration = true;
            int length = pageRoute.LastIndexOf( '/' );
            string prefix = length == -1 ? string.Empty : pageRoute.Substring( 0, length );
            model.Selectors.Add( CreateSelectorModel( prefix, routeTemplate ) );
        }

        internal bool TryParseAreaPath( string relativePath, out (string areaName, string viewEnginePath) result )
        {
            result = new ValueTuple<string, string>();
            int num = relativePath.IndexOf( '/', 1 );
            if ( num == -1 || num >= relativePath.Length - 1 || !relativePath.StartsWith( _normalizedAreaRootDirectory, StringComparison.OrdinalIgnoreCase ) )
            {
                _logger.UnsupportedAreaPath( relativePath );
                return false;
            }

            int indexA = relativePath.IndexOf( '/', num + 1 );
            if ( indexA == -1 || indexA == relativePath.Length )
            {
                _logger.UnsupportedAreaPath( relativePath );
                return false;
            }

            string str1 = relativePath.Substring( num + 1, indexA - num - 1 );
            if ( string.Compare( relativePath, indexA, "/Pages/", 0, "/Pages/".Length, StringComparison.OrdinalIgnoreCase ) != 0 )
            {
                _logger.UnsupportedAreaPath( relativePath );
                return false;
            }

            int startIndex = indexA + "/Pages/".Length - 1;
            string str2 = relativePath.Substring( startIndex, relativePath.Length - startIndex - RazorViewEngine.ViewExtension.Length );
            result = new ValueTuple<string, string>( str1, str2 );
            return true;
        }

        private string GetViewEnginePath( string rootDirectory, string path )
        {
            int startIndex = rootDirectory.Length;
            int num = path.Length - RazorViewEngine.ViewExtension.Length;
            return path.Substring( startIndex, num - startIndex );
        }

        private static string CreateAreaRoute( string areaName, string viewEnginePath )
        {
            var inplaceStringBuilder = new InplaceStringBuilder( 1 + areaName.Length + viewEnginePath.Length );
            inplaceStringBuilder.Append( '/' );
            inplaceStringBuilder.Append( areaName );
            inplaceStringBuilder.Append( viewEnginePath );
            return inplaceStringBuilder.ToString();
        }

        private static SelectorModel CreateSelectorModel( string prefix, string routeTemplate )
        {
            return new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel
                {
                    Template = AttributeRouteModel.CombineTemplates( prefix, routeTemplate )
                }
            };
        }
    }
}
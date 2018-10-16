namespace Tapas.Cms.FlatFile.Core.Actions
{
    using System;
    using ExtCore.Mvc.Infrastructure.Actions;
    using Git;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class UseBackendMvcAction: IUseMvcAction
    {
        public int Priority => 1;

        public void Execute( IRouteBuilder routeBuilder, IServiceProvider serviceProvider )
        {
            var options = serviceProvider.GetService<IOptions<FlatFileCmsGitOptions>>()?.Value;
//
//            string routePrefix = DetermineRoutePrefix( options );
//            routeBuilder.MapAreaRoute( "BackendDefault",
//                                       "Backend",
//                                       routePrefix + "{controller=Home}/{action=Index}/{id?}");
        }

//        private static string DetermineRoutePrefix( BackendOptions options )
//        {
//            string routePrefix;
//
//            if ( options == null )
//            {
//                routePrefix = "backend/";
//            }
//            else if ( options.RoutePrefix.IsNullOrWhiteSpace() )
//            {
//                routePrefix = string.Empty;
//            }
//            else
//            {
//                routePrefix = options.RoutePrefix + "/";
//            }
//
//            return routePrefix;
//        }
    }
}
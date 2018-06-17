namespace Tapas.Backend.Core.Actions
{
    using System;
    using ExtCore.Mvc.Infrastructure.Actions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public class UseBackendMvcAction: IUseMvcAction
    {
        public int Priority => 1;

        public void Execute( IRouteBuilder routeBuilder, IServiceProvider serviceProvider )
        {
            routeBuilder.MapAreaRoute( "BackendDefault",
                                       "Backend",
                                       "backend/{controller=Home}/{action=Index}/{id?}");
        }
    }
}
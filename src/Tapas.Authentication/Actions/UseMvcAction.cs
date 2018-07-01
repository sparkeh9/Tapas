namespace Tapas.Authentication.Actions
{
    using System;
    using ExtCore.Mvc.Infrastructure.Actions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public class UseMvcAction: IUseMvcAction
    {
        public int Priority => 1;

        public void Execute( IRouteBuilder routeBuilder, IServiceProvider serviceProvider )
        {
            routeBuilder.MapRoute( "users", "{controller=User}/{action=Login}" ); 
        }
    }
}
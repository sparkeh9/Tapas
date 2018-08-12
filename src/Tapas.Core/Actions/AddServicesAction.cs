namespace Tapas.Core.Actions
{
    using System;
    using System.Linq;
    using ExtCore.Infrastructure;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Security.Policy;

    public class AddServicesAction : IConfigureServicesAction
    {
        public int Priority => 1;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            serviceCollection.AddScoped<IUrlHelper>( x =>
                                                     {
                                                         var actionContext = x.GetService<IActionContextAccessor>().ActionContext;
                                                         return new UrlHelper( actionContext );
                                                     } );
            serviceCollection.AddAuthorization( options =>
                                                {
                                                    foreach ( var policy in ExtensionManager.GetInstances<IModuleAuthorisationPolicyFactory>().SelectMany( x => x.GetPolicies() ) )
                                                    {
                                                        options.AddPolicy( policy.Name, policy.Policy );
                                                    }
                                                }
                                              );
        }
    }
}
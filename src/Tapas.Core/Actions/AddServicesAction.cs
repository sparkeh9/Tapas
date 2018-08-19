namespace Tapas.Core.Actions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using ExtCore.Infrastructure;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyModel;
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

            var assembliesToExclude = new[]
            {
                "System",
                "Microsoft",
                "ExtCore",
                "AutoMapper",
                "runtime",
                "Newtonsoft",
                "Remotion",
                "FluentValidation",
                "MySql",
                "NETStandard.Library",
                "NuGet.Frameworks",
                "Pomelo"
            };
            var dependencyContext = DependencyContext.Default;
            var assemblies = dependencyContext.RuntimeLibraries
                                              .Where( lib => assembliesToExclude.All( x => !lib.Name.StartsWith( x ) ) )
                                              .SelectMany( lib => lib.GetDefaultAssemblyNames( dependencyContext )
                                                                     .Select( Assembly.Load ) )
                                              .ToList();
            serviceCollection.AddAutoMapper( assemblies );
        }
    }
}
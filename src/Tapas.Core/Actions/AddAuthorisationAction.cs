namespace Tapas.Core.Actions
{
    using System;
    using System.Linq;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.Extensions.DependencyInjection;
    using Security;
    using Security.Policy;

    public class AddAuthorisationAction : IConfigureServicesAction
    {
        public int Priority => 1;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            serviceCollection.AddAuthorization( options =>
                                                {
                                                    foreach ( var policy in ExtCore.Infrastructure.ExtensionManager.GetInstances<IModuleAuthorisationPolicyFactory>().SelectMany( x => x.GetPolicies() ) )
                                                    {
                                                        options.AddPolicy( policy.Name, policy.Policy );
                                                    }
                                                }
                                              );
        }
    }
}
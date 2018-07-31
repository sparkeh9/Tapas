namespace Tapas.Core.Actions
{
    using System;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.Extensions.DependencyInjection;
    using Security;

    public class AddAuthorisationAction : IConfigureServicesAction
    {
        public int Priority => 1;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            serviceCollection.AddAuthorization( options =>
                                                {
                                                    foreach ( var authorizationPolicyProvider in ExtCore.Infrastructure.ExtensionManager.GetInstances<IAuthorisationPolicyProvider>() )
                                                    {
                                                        options.AddPolicy( authorizationPolicyProvider.Name, authorizationPolicyProvider.GetAuthorisationPolicy() );
                                                    }
                                                }
                                              );
        }
    }
}
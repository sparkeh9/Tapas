namespace Tapas.Backend.Core.Security
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Tapas.Core.ExtensionMethods;
    using Tapas.Core.Security;
    using Tapas.Core.Security.Policy;

    public class BackendModuleAuthorisationPolicyFactory : IModuleAuthorisationPolicyFactory
    {
        private const string ModuleNamespace = "Backend";
        public IEnumerable<IAuthorisationPolicyProvider> GetPolicies()
        {
            foreach ( string permission in typeof( Permissions ).GetAllPublicConstantValues<string>() )
            {
                string backendPermission = $"{ModuleNamespace}:{permission}";
                yield return new GenericAuthorisationPolicyProvider( backendPermission, new AuthorizationPolicyBuilder()
                                                                                              .RequireAuthenticatedUser()
                                                                                              .AddAuthenticationSchemes(  )
                                                                                         .RequireClaim( ClaimTypes.Permission, backendPermission )
                                                                                         .Build() );
            }
        }
    }
}
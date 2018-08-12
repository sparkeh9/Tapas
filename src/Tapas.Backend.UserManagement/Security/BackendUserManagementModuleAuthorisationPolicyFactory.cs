﻿namespace Tapas.Backend.UserManagement.Security
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Tapas.Core.ExtensionMethods;
    using Tapas.Core.Security;
    using Tapas.Core.Security.Policy;

    public class BackendUserManagementModuleAuthorisationPolicyFactory : IModuleAuthorisationPolicyFactory
    {
        private const string ModuleNamespace = "Backend:Users";
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
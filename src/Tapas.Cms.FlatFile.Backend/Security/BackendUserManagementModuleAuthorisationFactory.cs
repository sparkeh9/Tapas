﻿namespace Tapas.Cms.FlatFile.Backend.Security
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Tapas.Backend.Core.Security;
    using Tapas.Core.ExtensionMethods;
    using Tapas.Core.Security.Policy;

    public class BackendUserManagementModuleAuthorisationFactory : IModuleAuthorisationFactory
    {
        private const string ModuleNamespace = "Backend:Pages";

        public IEnumerable<Claim> GetClaims()
        {
            foreach ( string permission in typeof( Permissions ).GetAllPublicConstantValues<string>() )
            {
                yield return new Claim( "Permission", $"{ModuleNamespace}:{permission}" );
            }
        }

        public IEnumerable<IAuthorisationPolicyProvider> GetPolicies()
        {
            foreach ( string permission in typeof( Permissions ).GetAllPublicConstantValues<string>() )
            {
                string backendPermission = $"{ModuleNamespace}:{permission}";
                yield return new GenericAuthorisationPolicyProvider( backendPermission, new AuthorizationPolicyBuilder()
                                                                                        .RequireAuthenticatedUser()
                                                                                        .AddAuthenticationSchemes()
                                                                                        .AddRequirements( new ClaimOrSuperAdminRequirement( backendPermission ) )
                                                                                        .Build() );
            }
        }
    }
}
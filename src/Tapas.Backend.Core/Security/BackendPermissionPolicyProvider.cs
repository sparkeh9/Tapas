namespace Tapas.Backend.Core.Security
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class BackendPermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "Backend";

        public Task<AuthorizationPolicy> GetPolicyAsync( string policyName )
        {
            if ( !policyName.StartsWith( POLICY_PREFIX, StringComparison.OrdinalIgnoreCase ) )
            {
                return GetDefaultPolicyAsync();
            }

            var policy = new AuthorizationPolicyBuilder();
            policy.RequireAuthenticatedUser();
            policy.RequireRole( policyName );
            return Task.FromResult( policy.Build() );
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
    }
}
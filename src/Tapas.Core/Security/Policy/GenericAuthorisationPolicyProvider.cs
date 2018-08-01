namespace Tapas.Core.Security.Policy
{
    using Microsoft.AspNetCore.Authorization;

    public class GenericAuthorisationPolicyProvider : IAuthorisationPolicyProvider
    {
        public string Name { get; }
        public AuthorizationPolicy Policy { get; }

        public GenericAuthorisationPolicyProvider( string name, AuthorizationPolicy policy )
        {
            Name = name;
            Policy = policy;
        }
    }
}
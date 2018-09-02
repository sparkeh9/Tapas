namespace Tapas.Core.Security.Policy
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface IModuleAuthorisationFactory
    {
        IEnumerable<Claim> GetClaims();
        IEnumerable<IAuthorisationPolicyProvider> GetPolicies();
    }
}
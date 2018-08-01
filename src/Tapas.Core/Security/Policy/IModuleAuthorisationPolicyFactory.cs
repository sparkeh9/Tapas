namespace Tapas.Core.Security.Policy
{
    using System.Collections.Generic;

    public interface IModuleAuthorisationPolicyFactory
    {
        IEnumerable<IAuthorisationPolicyProvider> GetPolicies();
    }

}
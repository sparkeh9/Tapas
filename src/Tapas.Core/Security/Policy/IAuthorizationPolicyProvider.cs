namespace Tapas.Core.Security.Policy
{
    using Microsoft.AspNetCore.Authorization;

    public interface IAuthorisationPolicyProvider
    {
        string Name { get; }
        AuthorizationPolicy Policy { get; }
    }
}
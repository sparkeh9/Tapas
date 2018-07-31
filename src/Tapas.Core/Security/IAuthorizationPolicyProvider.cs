namespace Tapas.Core.Security
{
    using Microsoft.AspNetCore.Authorization;

    public interface IAuthorisationPolicyProvider
    {
        string Name { get; }
        AuthorizationPolicy GetAuthorisationPolicy();
    }
}
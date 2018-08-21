namespace Tapas.Backend.Core.Security
{
    using Microsoft.AspNetCore.Authorization;

    public class ClaimOrSuperAdminRequirement: IAuthorizationRequirement
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; }

        public ClaimOrSuperAdminRequirement(string claimValue)
        {
            ClaimValue = claimValue;
        }
    }
}
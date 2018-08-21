namespace Tapas.Backend.Core.Security
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class ClaimOrSuperAdminHandler : AuthorizationHandler<ClaimOrSuperAdminRequirement>
    {
        protected override Task HandleRequirementAsync( AuthorizationHandlerContext context, ClaimOrSuperAdminRequirement requirement )
        {
            if ( context.User.IsInRole( "SuperAdmin" ) || context.User.HasClaim(c => c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
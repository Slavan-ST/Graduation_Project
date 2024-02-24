using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Helper.Security.Requirements;

namespace Helper.Security.Handlers
{
    public class AccessHandler : AuthorizationHandler<AccessRequirement>
    {
        public AccessHandler()
        {

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessRequirement requirement)
        {
            bool hasClaim = context.User.HasClaim(c => c.Type == ClaimTypes.Role);
            if (!hasClaim)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            string claimValue = context.User.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;
            if (claimValue == requirement.Role)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebAPI.Security.Requirements;

namespace WebAPI.Security.Handlers
{
    /// <summary>
    /// Класс обработчика доступа к определенным запросам
    /// </summary>
    public class AccessHandler : AuthorizationHandler<AccessRequirement>
    {
        /// <summary>
        /// Конструктор обработчика доступа к определенным запросам
        /// </summary>
        public AccessHandler()
        {

        }
        /// <summary>
        /// Обработка разрешений
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
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

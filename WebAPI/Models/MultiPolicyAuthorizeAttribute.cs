using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MultiPolicyAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _policies;

        public MultiPolicyAuthorizeAttribute(params string[] policies)
        {
            _policies = policies;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<IAuthorizationService>();

            if (authService == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            bool isAuthorized = false;
            foreach (var policy in _policies)
            {
                var authorized = await authService.AuthorizeAsync(context.HttpContext.User, policy);
                if (authorized.Succeeded)
                {
                    isAuthorized = true;
                    break;
                }
            }

            if (!isAuthorized)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Security.Requirements
{
    public class AccessRequirement : IAuthorizationRequirement
    {
        public AccessRequirement(string role)
        {
            Role = role;
        }
        public string Role { get; private set; }
    }
}

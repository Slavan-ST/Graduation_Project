using Microsoft.AspNetCore.Authorization;
using WebAPI.Models.Data;

namespace WebAPI.Models.Requirements
{
    public class AccessRequirement: IAuthorizationRequirement
    {
        public AccessRequirement(string role)
        {
            Role = role;
        }
        public string Role { get; private set; }
    }
}

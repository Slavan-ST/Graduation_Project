using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Security.Requirements
{
    /// <summary>
    /// Атрибут доступа
    /// </summary>
    public class AccessRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="role"></param>
        public AccessRequirement(string role)
        {
            Role = role;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Role { get; private set; }
    }
}

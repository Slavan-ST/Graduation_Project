using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для обработки выхода пользователя
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SignOutController : ControllerBase
    {
        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}

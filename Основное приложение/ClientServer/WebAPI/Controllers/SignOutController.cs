using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignOutController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> SignInAsync()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}

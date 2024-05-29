using Helper.Models.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Test
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        /// <summary>
        /// отправка текстовых сообщений серверу
        /// </summary>
        /// <param name="message"></param>
        [HttpGet("{message}")]
        public HttpStatusCode GetMessage(string message)
        {
            Console.WriteLine(message);
            return HttpStatusCode.OK;
        }
    }

        /// <summary>
        /// Контроллер для обработки авторизации
        /// </summary>
        [ApiController]
    public class HomeController : ControllerBase
    {

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("Home/SignIn/")]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> SignInAsync(string login, string password)
        {
            ApplicationContext db = new();

            var user = await db.Users
                .Include(c => c.Role)
                .Where(x => x.Login == login)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new UserDTO() { Name = "^not found^"};
            }
            if (!SecretHasher.Verify(password, user.Password))
            {
                return new UserDTO() { Name = "^not found^" }; ;
            }


            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, login),
                new(ClaimTypes.Role, user.Role!.Name)
            };

            ClaimsIdentity claimsIdentity = new(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            UserDTO userDTO = new(user);

            return new JsonResult(userDTO);
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        [Route("Home/SignOut")]
        [HttpGet]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        [HttpGet]
        public IActionResult TestConnect()
        {
            return new JsonResult(System.Net.HttpStatusCode.OK);
        }
    }
}

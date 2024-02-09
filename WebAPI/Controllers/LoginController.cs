using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Login(string login, string password)
        {
            ApplicationContext db = new ApplicationContext();
            var user = await db.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, login)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Redirect("/users/1");
        }
    }
}

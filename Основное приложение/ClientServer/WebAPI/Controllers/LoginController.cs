using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Security.Claims;
using WebAPI.Security;
using WebAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Helper.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<UserDTO>> SignInAsync(string login, string password)
        {
            ApplicationContext db = new ApplicationContext();

            var user = await db.Users
                .Include(c => c.Role)
                .Where(x => x.Login == login)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return StatusCode(403, "Неверный логин или пароль!");
            }
            if (!SecretHasher.Verify(password, user.Password))
            {
                return StatusCode(403, "Неверный логин или пароль!");
            }


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, user.Role!.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            UserDTO userDTO = new UserDTO(user);

            return new JsonResult(userDTO);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Security;
using WebAPI.Data;
using WebAPI.Models.Main;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            ApplicationContext db = new ApplicationContext();
            var user = await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            db.Dispose();
            return new JsonResult(user);
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers(params string[] search)
        {
            List<User> users = new List<User>();
            ApplicationContext db = new ApplicationContext();

            foreach (var parameter in search)
            {
                var usersSearch = await db.Users.Where(x => x.Login.Contains(parameter)).ToListAsync();
                if (usersSearch != null)
                {
                    users.AddRange(usersSearch);
                }
            }

            db.Dispose();
            if (users.Count > 0)
            {
                return new JsonResult(users);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(User userFromClient) //новый юзверь
        {
            if (userFromClient == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            bool existUser = await db.Users
                .Where(x => x.Login == userFromClient.Login)
                .FirstOrDefaultAsync() != null;

            if (existUser)
            {
                return StatusCode(409, "объект уже существует");
            }

            userFromClient.Password = SecretHasher.Hash(userFromClient.Password);

            await db.Users.AddAsync(userFromClient);
            await db.SaveChangesAsync();
            db.Dispose();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutUser(User userFromClient)
        {
            if (userFromClient == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (!await db.Users.ContainsAsync(userFromClient))
            {
                return StatusCode(404);
            }
            db.Users.Update(userFromClient);
            await db.SaveChangesAsync();
            db.Dispose();
            return StatusCode(202);//принято
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(User userFromClient)
        {
            if (userFromClient == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (!await db.Users.ContainsAsync(userFromClient))
            {
                return StatusCode(404);
            }
            db.Users.Remove(userFromClient);
            await db.SaveChangesAsync();
            db.Dispose();
            return StatusCode(202);//принято
        }
    }
}

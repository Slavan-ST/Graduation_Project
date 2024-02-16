using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Authentication;
using WebAPI.Data;
using WebAPI.Models;

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
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            ApplicationContext db = new ApplicationContext();
            var users = await db.Users.ToListAsync();
            if (users == null)
            {
                return NotFound();
            }
            db.Dispose();
            return new JsonResult(users);
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(User userFromClient)
        {
            if (userFromClient == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (await db.Users.ContainsAsync(userFromClient))
            {
                return StatusCode(400);
            }
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

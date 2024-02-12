using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.Data;

namespace WebAPI.Controllers
{
    // /users    
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        //получение пользователя
        [Authorize(Policy = "user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            ApplicationContext db = ApplicationContext.GetContext();
            var user = await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            db.Dispose();
            return new JsonResult(user);
        }
        //добавление пользователя        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            if (user == null)
            {
                return NoContent();
            }
            ApplicationContext db = ApplicationContext.GetContext();
            if (await db.Users.ContainsAsync(user))
            {
                return StatusCode(400);
            }
            await db.Users.AddAsync(user);
            db.Dispose();
            return StatusCode(201);
        }
        //обновление пользователя        
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Put(User user)
        {
            if (user == null)
            {
                return NoContent();
            }
            ApplicationContext db = ApplicationContext.GetContext();
            if (!await db.Users.ContainsAsync(user))
            {
                return StatusCode(404);
            }
            db.Users.Update(user);
            db.Dispose();
            return StatusCode(202);//принято
        }
        //удаление пользователя        
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete(User user)
        {
            if (user == null)
            {
                return NoContent();
            }
            ApplicationContext db = ApplicationContext.GetContext();
            if (!await db.Users.ContainsAsync(user))
            {
                return StatusCode(404);
            }
            db.Users.Remove(user);
            db.Dispose();
            return StatusCode(202);//принято
        }
    }
}

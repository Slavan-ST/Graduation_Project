using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Security;
using WebAPI.Data;
using Helper.Models.DTO;
using Helper.Models.Main;
using Helper.Converters;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{login}")]
        public async Task<ActionResult<UserDTO>> GetUser(string login)
        {
            ApplicationContext db = new ApplicationContext();
            var user = await db.Users
                .Include(c => c.Role)
                .Where(x => x.Login == login)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            UserDTO userDTO = new UserDTO(user);

            return new JsonResult(userDTO);
        }


        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            ApplicationContext db = new ApplicationContext();
            var users = await db.Users
                .Include(c => c.Role)
                .ToListAsync();

            await db.DisposeAsync();

            if (users.Count <= 0)
            {
                return NotFound();
            }

            List<UserDTO> usersDTO = new List<UserDTO>();
            foreach (var user in  users)
            {
                usersDTO.Add(new UserDTO(user));
            }

            return new JsonResult(usersDTO);
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(UserChangedDTO? userDTO) //новый юзверь
        {
            if (userDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            var user = await db.Users
                .Include(c => c.Role)
                .Where(x => x.Login == userDTO.Login)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return StatusCode(409, "объект уже существует");
            }
            user = ConverterDTO.UserFromChangedDTO(userDTO);
            user.Password = SecretHasher.Hash(user.Password);

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutUser(UserChangedDTO userDTO)
        {
            if (userDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();


            //проверка на существование такой записи в БД
            User? user = await db.Users
                .Include(c => c.Role)
                .Where(x => x.Id == userDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return StatusCode(404);
            }

            user = ConverterDTO.UserFromChangedDTO(userDTO)!;
            user.Password = SecretHasher.Hash(user.Password);

            db.Users.Update(user);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }

        [HttpDelete("{login}")]
        public async Task<ActionResult> DeleteUser(string login)
        {
            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            User? user = await db.Users
                .Include(c => c.Role)
                .Where(x => x.Login == login)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return StatusCode(404);
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202, "пользователь удалён");//принято
        }
    }
}

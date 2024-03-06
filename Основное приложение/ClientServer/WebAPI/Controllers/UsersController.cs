using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Helper.Security;
using Helper.Data;
using Helper.Models.Main;
using Helper.Models.DTO;
using Helper.Converters;

namespace Helper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            ApplicationContext db = new ApplicationContext();
            var user = await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
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
            var users = await db.Users.ToListAsync();

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
        public async Task<ActionResult> PostUser(UserDTO? userDTO, string password) //новый юзверь
        {
            if (userDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            bool existUser = await db.Users.Where(x => x.Login == userDTO.Login).FirstOrDefaultAsync() != null;

            if (existUser)
            {
                return StatusCode(409, "объект уже существует");
            }
            User user = ConverterDTO.UserFromDTO(userDTO, password);
            user.Password = SecretHasher.Hash(user.Password);

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutUser(UserDTO userDTO, string? password = null)
        {
            if (userDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();


            //проверка на существование такой записи в БД
            User? user = await db.Users.Where(x => x.Id == userDTO.Id).FirstOrDefaultAsync();
            if (user == null)
            {
                return StatusCode(404);
            }
            if (password == null)
            {
                password = user.Password;
            }

            user = ConverterDTO.UserFromDTO(userDTO, password)!;

            db.Users.Update(user);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(User userDTO)
        {
            if (userDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            User? user = await db.Users.Where(x => x.Id == userDTO.Id).FirstOrDefaultAsync();
            if (user == null)
            {
                return StatusCode(404);
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

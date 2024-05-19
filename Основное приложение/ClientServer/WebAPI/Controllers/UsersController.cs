using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Security;
using WebAPI.Data;
using Helper.Models.DTO;
using Helper.Models.Main;
using Helper.Converters;
using Microsoft.Data.SqlClient;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Получение пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Пользователь без пароля</returns>
        [HttpGet("{login}")]
        public async Task<ActionResult<User>> GetUser(string login)
        {
            ApplicationContext db = new();
            var user = await db.Users
                .Include(c => c.Role)
                .Where(x => x.Login == login)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();


            return new JsonResult(user);
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns>Пользователи(без пароля)</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            ApplicationContext db = new();
            var users = await db.Users
                .Include(c => c.Role)
                .ToListAsync();

            await db.DisposeAsync();

            if (users.Count <= 0)
            {
                return NotFound();
            }

            return new JsonResult(users);
        }
        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostUser(User? userDTO)
        {
            if (userDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new();

            var user = await db.Users
                .Where(x => x.Login == userDTO.Login)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return StatusCode(409);
            }

            try
            {
                userDTO.Password = SecretHasher.Hash(userDTO.Password);

                await db.Database.ExecuteSqlRawAsync(
                    @"insert into Users (Name, Surname, Patronymic, Login, Password, RoleId) values 
                     (@name, @sname, @pat, @phone, @gender, @role);",

                    new SqlParameter("@name", userDTO.Name),
                    new SqlParameter("@sname", userDTO.Surname),
                    new SqlParameter("@pat", userDTO.Patronymic),
                    new SqlParameter("@phone", userDTO.Login),
                    new SqlParameter("@gender", userDTO.Password),
                    new SqlParameter("@role", userDTO.RoleId)

                    );
                await db.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(409);
            }
            await db.DisposeAsync();

            return new JsonResult(userDTO.Id);
        }
        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> PutUser(User userDTO)
        {
            if (userDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new();


            //проверка на существование такой записи в БД
            User? user = await db.Users
                .Where(x => x.Login == userDTO.Login)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return StatusCode(404);
            }

            user = userDTO;
            user.Password = SecretHasher.Hash(user.Password);

            db.Users.Update(user);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpGet("{login}&{newPassword}")]
        public async Task<ActionResult> PutUser(string login, string newPassword)
        {
            if (login == null || newPassword == null)
            {
                return NoContent();
            }
            ApplicationContext db = new();


            //проверка на существование такой записи в БД
            User? user = await db.Users
                .Where(x => x.Login == login)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return StatusCode(404);
            }

            user.Password = SecretHasher.Hash(newPassword);

            db.Users.Update(user);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpDelete("{login}")]
        public async Task<ActionResult> DeleteUser(string login)
        {
            ApplicationContext db = new();

            //проверка на существование такой записи в БД
            User? user = await db.Users
                .Where(x => x.Login == login)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return StatusCode(404);
            }

            try
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
            catch
            {
                Debug.WriteLine("Error delete user");
            }
            await db.DisposeAsync();

            return StatusCode(202, "пользователь удалён");//принято
        }
    }
}

using Helper.Converters;
using Helper.Models.DTO;
using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с комнатами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        /// <summary>
        /// Получение всех комнат 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            ApplicationContext db = new();
            var roles = await db.Roles
                //.Include(x => x.Students)
                .ToListAsync();

            if (roles == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(roles);
        }
        /// <summary>
        /// Получение комнаты по номеру
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<Role>> GetRole(string name)
        {
            ApplicationContext applicationContext = new();
            ApplicationContext db = applicationContext;
            var role = await db.Roles
                //.Include(x => x.Students)
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();

            if (role == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();


            return new JsonResult(role);
        }
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostRole(Role? roleDTO)
        {
            if (roleDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new();

            //проверка на существование такой записи в БД
            Role? role = await db.Roles
                //.Include(x => x.Students)
                .Where(x => x.Id == roleDTO.Id || x.Name == roleDTO.Name)
                .FirstOrDefaultAsync();

            if (role != null)
            {
                return StatusCode(409);
            }

            role = roleDTO;

            await db.Roles.AddAsync(role!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return new JsonResult(role.Id);
        }
        /// <summary>
        /// Изменение комнаты
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> PutRole(Role? roleDTO)
        {
            if (roleDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new();
            //проверка на существование такой записи в БД
            Role? role = await db.Roles
                .Include(x => x.Users)
                .Where(x => x.Id == roleDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (role == null)
            {
                return StatusCode(404);
            }

            role = roleDTO;

            db.Roles.Update(role!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
        /// <summary>
        /// Удаление комнаты
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            ApplicationContext db = new();

            var role = await db.Roles
                .Include(x => x.Users)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (role == null)
            {
                return StatusCode(404);
            }

            if (role.Users != null)
            {
                if (role.Users.Count() > 0)
                {
                    return StatusCode(409);
                }
            }

            db.Roles.Remove(role);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

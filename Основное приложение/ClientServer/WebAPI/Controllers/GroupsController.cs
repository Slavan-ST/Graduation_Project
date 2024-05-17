using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с группами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        /// <summary>
        /// Получение всех групп
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            ApplicationContext db = new ApplicationContext();
            var groups = await db.Groups.ToListAsync();
            if (groups == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(groups);
        }

        /// <summary>
        /// Добавление новой группы
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostGroup(Group? groupDTO)
        {
            if (groupDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            Group? group = await db.Groups
                .Where(x => x.Id == groupDTO.Id)
                .FirstOrDefaultAsync();

            if (group != null)
            {
                return StatusCode(400);
            }

            group = groupDTO;

            await db.Groups.AddAsync(group);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return new JsonResult(group.Id);
        }
        /// <summary>
        /// Изменение группы
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> PutGroup(Group? groupDTO)
        {
            if (groupDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();
            //проверка на существование такой записи в БД
            Group? group = await db.Groups
                .Where(x => x.Id == groupDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (group == null)
            {
                return StatusCode(404);
            }

            group = groupDTO;

            db.Groups.Update(group!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
        /// <summary>
        /// Удаление группы
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteGroup(string name)
        {
            ApplicationContext db = new ApplicationContext();

            var group = await db.Groups
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();

            if (group == null)
            {
                return StatusCode(404);
            }

            db.Groups.Remove(group);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы со статусами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StatusesController : ControllerBase
    {
        /// <summary>
        /// Получение всех статусов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            ApplicationContext db = new();
            var statuses = await db.Statuses.Include(x => x.Students).ToListAsync();
            if (statuses == null)
            {
                await db.DisposeAsync();
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(statuses);
        }

        /// <summary>
        /// Добавление статуса
        /// </summary>
        /// <param name="statusDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostStatus(Status? statusDTO)
        {
            if (statusDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new();

            //проверка на существование такой записи в БД
            Status? status = await db.Statuses
                .Where(x => x.Id == statusDTO.Id || x.Name == statusDTO.Name)
                .FirstOrDefaultAsync();

            if (status != null)
            {
                await db.DisposeAsync();
                return StatusCode(409);
            }

            status = statusDTO;

            await db.Statuses.AddAsync(status);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return new JsonResult(status.Id);
        }
        /// <summary>
        /// Обновление статуса
        /// </summary>
        /// <param name="statusDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> PutStatus(Status? statusDTO)
        {
            if (statusDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new();
            //проверка на существование такой записи в БД
            Status? status = await db.Statuses
                .Where(x => x.Id == statusDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (status == null)
            {
                await db.DisposeAsync();
                return StatusCode(404);
            }

            status = statusDTO;

            db.Statuses.Update(status!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
        /// <summary>
        /// Удаление статуса
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
            ApplicationContext db = new();

            var status = await db.Statuses
                .Include(x => x.Students)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (status == null)
            {
                await db.DisposeAsync();
                return StatusCode(404);
            }
            if (status.Students != null)
            {
                if (status.Students.Count > 0)
                {
                    await db.DisposeAsync();
                    return StatusCode(409);
                }
            }

            db.Statuses.Remove(status);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

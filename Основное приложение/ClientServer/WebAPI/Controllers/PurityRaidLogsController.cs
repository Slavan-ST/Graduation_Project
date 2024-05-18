using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с логами журнала рейда чистоты
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PurityRaidLogsController : ControllerBase
    {
        /// <summary>
        /// Получение всех записей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurityRaidLog>>> GetPurityRaidLogs()
        {
            ApplicationContext db = new();
            var purityRaidLogs = await db.PurityRaidLogs
                .Include(c => c.Room)
                .ToListAsync();

            if (purityRaidLogs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(purityRaidLogs);
        }
        /// <summary>
        /// Получение записей за указанный день
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("day:{day}.{month}.{year}")]
        public async Task<ActionResult<IEnumerable<PurityRaidLog>>> GetPurityRaidLogsDay(int day, int month, int year)
        {
            ApplicationContext db = new();
            var purityRaidLogs = await db.PurityRaidLogs
                .Include(c => c.Room)
                .Where(c => c.Date.Year == year && c.Date.Month == month && c.Date.Day == day)
                .ToListAsync();

            if (purityRaidLogs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(purityRaidLogs);
        }
        /// <summary>
        /// Получение записей за указанный месяц
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("month:{month}.{year}")]
        public async Task<ActionResult<IEnumerable<PurityRaidLog>>> GetPurityRaidLogsMonth(int month, int year)
        {
            ApplicationContext db = new();
            var purityRaidLogs = await db.PurityRaidLogs
                .Include(c => c.Room)
                .Where(c => c.Date.Year == year && c.Date.Month == month)
                .ToListAsync();

            if (purityRaidLogs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(purityRaidLogs);
        }
        /// <summary>
        /// Получение записей за указанный год
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("year:{year}")]
        public async Task<ActionResult<IEnumerable<PurityRaidLog>>> GetPurityRaidLogsYear(int year)
        {
            ApplicationContext db = new();
            var purityRaidLogs = await db.PurityRaidLogs
                .Include(c => c.Room)
                .Where(c => c.Date.Year == year)
                .ToListAsync();

            if (purityRaidLogs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(purityRaidLogs);
        }
        /// <summary>
        /// Получение записей по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PurityRaidLog>> GetPurityRaidLog(int id)
        {
            ApplicationContext db = new();
            var purityRaidLog = await db.PurityRaidLogs
                .Include(c => c.Room)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (purityRaidLog == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(purityRaidLog);
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="purityRaidLogDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostPurityRaidLog(PurityRaidLog purityRaidLogDTO)
        {
            if (purityRaidLogDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new();

            //проверка на существование такой записи в БД
            PurityRaidLog? purityRaidLog = await db.PurityRaidLogs
                .Where(x => x.Id == purityRaidLogDTO.Id)
                .FirstOrDefaultAsync();

            if (purityRaidLog != null)
            {
                return StatusCode(409);
            }

            await db.PurityRaidLogs.AddAsync(purityRaidLogDTO);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return new JsonResult(purityRaidLogDTO.Id);
        }
        /// <summary>
        /// Изменение указанной записи
        /// </summary>
        /// <param name="purityRaidLogDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> PutPurityRaidLog(PurityRaidLog purityRaidLogDTO)
        {
            if (purityRaidLogDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new();

            var purityRaidLog = await db.PurityRaidLogs
                .Where(x => x.Id == purityRaidLogDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (purityRaidLog == null)
            {
                return StatusCode(404);
            }

            purityRaidLog = purityRaidLogDTO!;

            db.Update(purityRaidLog);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
        /// <summary>
        /// Удаление записи по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurityRaidLog(int id)
        {
            ApplicationContext db = new();

            var purityRaidLog = await db.PurityRaidLogs
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (purityRaidLog == null)
            {
                return StatusCode(404);
            }
            db.PurityRaidLogs.Remove(purityRaidLog);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

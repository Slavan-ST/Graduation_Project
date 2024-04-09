using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurityRaidLogsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetPurityRaidLogs()
        {
            ApplicationContext db = new ApplicationContext();
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
        [HttpGet("day:{day}.{month}.{year}")]
        public async Task<ActionResult> GetPurityRaidLogsDay(int day, int month, int year)
        {
            ApplicationContext db = new ApplicationContext();
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
        [HttpGet("month:{month}.{year}")]
        public async Task<ActionResult> GetPurityRaidLogsMonth(int month, int year)
        {
            ApplicationContext db = new ApplicationContext();
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
        [HttpGet("year:{year}")]
        public async Task<ActionResult> GetPurityRaidLogsYear(int year)
        {
            ApplicationContext db = new ApplicationContext();
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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPurityRaidLog(int id)
        {
            ApplicationContext db = new ApplicationContext();
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

        [HttpPost]
        public async Task<ActionResult> PostPurityRaidLog(PurityRaidLog purityRaidLogDTO)
        {
            if (purityRaidLogDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            PurityRaidLog? purityRaidLog = await db.PurityRaidLogs.Where(x => x.Id == purityRaidLogDTO.Id).FirstOrDefaultAsync();
            if (purityRaidLog != null)
            {
                return StatusCode(400);
            }

            if (await db.PurityRaidLogs.ContainsAsync(purityRaidLog))
            {
                return StatusCode(400);
            }
            await db.PurityRaidLogs.AddAsync(purityRaidLogDTO);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutPurityRaidLog(PurityRaidLog purityRaidLogDTO)
        {
            if (purityRaidLogDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurityRaidLog(int id)
        {
            ApplicationContext db = new ApplicationContext();

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

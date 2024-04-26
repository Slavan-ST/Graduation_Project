using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DutyScheduleController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetDutySchedule()
        {
            ApplicationContext db = new ApplicationContext();
            var dutySchedules = await db.DutySchedule
                .Include(c => c.Student)
                .Include(c => c.Student!.Room)
                .ToListAsync();

            if (dutySchedules == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(dutySchedules);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("day:{day}.{month}.{year}")]
        public async Task<ActionResult> GetDutyScheduleDay(int day, int month, int year)
        {
            ApplicationContext db = new ApplicationContext();
            var dutySchedules = await db.DutySchedule
                .Include(c => c.Student)
                .Include(c => c.Student!.Room)
                .Where(c => c.Date.Year == year && c.Date.Month == month && c.Date.Day == day)
                .ToListAsync();

            if (dutySchedules == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(dutySchedules);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("month:{month}.{year}")]
        public async Task<ActionResult> GetDutyScheduleMonth(int month, int year)
        {
            ApplicationContext db = new ApplicationContext();
            var dutySchedules = await db.DutySchedule
                .Include(c => c.Student)
                .Include(c => c.Student!.Room)
                .Where(c => c.Date.Year == year && c.Date.Month == month)
                .ToListAsync();

            if (dutySchedules == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(dutySchedules);
        }
        [HttpGet("year:{year}")]
        public async Task<ActionResult> GetDutyScheduleYear(int year)
        {
            ApplicationContext db = new ApplicationContext();
            var dutySchedules = await db.DutySchedule
                .Include(c => c.Student)
                .Include(c => c.Student!.Room)
                .Where(c => c.Date.Year == year)
                .ToListAsync();

            if (dutySchedules == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(dutySchedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDutySchedule(int id)
        {
            ApplicationContext db = new ApplicationContext();
            var dutySchedule = await db.DutySchedule
                .Include(c => c.Student)
                .Include(c => c.Student!.Room)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (dutySchedule == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(dutySchedule);
        }

        [HttpPost]
        public async Task<ActionResult> PostDutySchedule(DutySchedule dutyScheduleDTO)
        {
            if (dutyScheduleDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            DutySchedule? dutySchedule = await db.DutySchedule.Where(x => x.Id == dutyScheduleDTO.Id).FirstOrDefaultAsync();
            if (dutySchedule != null)
            {
                return StatusCode(400);
            }

            if (await db.DutySchedule.ContainsAsync(dutySchedule))
            {
                return StatusCode(400);
            }
            await db.DutySchedule.AddAsync(dutyScheduleDTO);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutDutySchedule(DutySchedule dutyScheduleDTO)
        {
            if (dutyScheduleDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            var dutySchedule = await db.DutySchedule
                .Where(x => x.Id == dutyScheduleDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (dutySchedule == null)
            {
                return StatusCode(404);
            }

            dutySchedule = dutyScheduleDTO!;

            db.Update(dutySchedule);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDutySchedule(int id)
        {
            ApplicationContext db = new ApplicationContext();

            var dutySchedule = await db.DutySchedule
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (dutySchedule == null)
            {
                return StatusCode(404);
            }
            db.DutySchedule.Remove(dutySchedule);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

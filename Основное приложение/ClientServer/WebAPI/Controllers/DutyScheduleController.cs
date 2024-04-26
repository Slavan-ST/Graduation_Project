using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с графиком дежурств
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DutyScheduleController : ControllerBase
    {
        /// <summary>
        /// Получение всех логов графика
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DutySchedule>>> GetDutySchedule()
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
        /// Получение графика на указанный день
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("day:{day}.{month}.{year}")]
        public async Task<ActionResult<IEnumerable<DutySchedule>>> GetDutyScheduleDay(int day, int month, int year)
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
        /// Получение графика на указанный месяц
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("month:{month}.{year}")]
        public async Task<ActionResult<IEnumerable<DutySchedule>>> GetDutyScheduleMonth(int month, int year)
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
        /// <summary>
        /// Получение графика на указанный год
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("year:{year}")]
        public async Task<ActionResult<IEnumerable<DutySchedule>>> GetDutyScheduleYear(int year)
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

        /// <summary>
        /// Получение лога графика по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DutySchedule>> GetDutySchedule(int id)
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
        /// <summary>
        /// Добавление новой записи графика
        /// </summary>
        /// <param name="dutyScheduleDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Обновление записи графика
        /// </summary>
        /// <param name="dutyScheduleDTO"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Удаление записи по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

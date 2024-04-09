using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DutyScheduleController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DutySchedule>>> GetDutySchedule()
        {
            ApplicationContext db = new ApplicationContext();
            var dutySchedules = await db.DutySchedule.ToListAsync();
            if (dutySchedules == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(dutySchedules);
        }


        [HttpPost]
        public async Task<ActionResult> PostDutySchedule(DutySchedule? dutyScheduleDTO)
        {
            if (dutyScheduleDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            DutySchedule? dutySchedule = await db.DutySchedule
                .Where(x => x.Id == dutyScheduleDTO.Id)
                .FirstOrDefaultAsync();

            if (dutySchedule != null)
            {
                return StatusCode(400);
            }

            dutySchedule = dutyScheduleDTO;

            await db.DutySchedule.AddAsync(dutySchedule!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutDutySchedule(DutySchedule? dutyScheduleDTO)
        {
            if (dutyScheduleDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();
            //проверка на существование такой записи в БД
            DutySchedule? dutySchedule = await db.DutySchedule
                .Where(x => x.Id == dutyScheduleDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (dutySchedule == null)
            {
                return StatusCode(404);
            }

            dutySchedule = dutyScheduleDTO;

            db.DutySchedule.Update(dutySchedule!);
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

using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventO>>> GetEvents()
        {
            ApplicationContext db = new ApplicationContext();
            var eventOs = await db.Events.ToListAsync();
            if (eventOs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(eventOs);
        }


        [HttpPost]
        public async Task<ActionResult> PostEvent(EventO? eventODTO)
        {
            if (eventODTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            EventO? eventO = await db.Events
                .Where(x => x.Id == eventODTO.Id)
                .FirstOrDefaultAsync();

            if (eventO != null)
            {
                return StatusCode(400);
            }

            eventO = eventODTO;

            await db.Events.AddAsync(eventO!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutEvent(EventO? eventODTO)
        {
            if (eventODTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();
            //проверка на существование такой записи в БД
            EventO? eventO = await db.Events
                .Where(x => x.Id == eventODTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (eventO == null)
            {
                return StatusCode(404);
            }

            eventO = eventODTO;

            db.Events.Update(eventO!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            ApplicationContext db = new ApplicationContext();

            var eventO = await db.Events
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (eventO == null)
            {
                return StatusCode(404);
            }

            db.Events.Remove(eventO);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetStatuses()
        {
            ApplicationContext db = new ApplicationContext();
            var statuss = await db.Statuses.ToListAsync();
            if (statuss == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(statuss);
        }


        [HttpPost]
        public async Task<ActionResult> PostStatus(Status? statusDTO)
        {
            if (statusDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            Status? status = await db.Statuses
                .Where(x => x.Id == statusDTO.Id)
                .FirstOrDefaultAsync();

            if (status != null)
            {
                return StatusCode(400);
            }

            status = statusDTO;

            await db.Statuses.AddAsync(status!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutStatus(Status? statusDTO)
        {
            if (statusDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();
            //проверка на существование такой записи в БД
            Status? status = await db.Statuses
                .Where(x => x.Id == statusDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return StatusCode(404);
            }

            status = statusDTO;

            db.Statuses.Update(status!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
            ApplicationContext db = new ApplicationContext();

            var status = await db.Statuses
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return StatusCode(404);
            }

            db.Statuses.Remove(status);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

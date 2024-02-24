using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Helper.Data;
using Helper.Models.Main;

namespace Helper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            ApplicationContext db = new ApplicationContext();
            var rooms = await db.Rooms.ToListAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            db.Dispose();
            return new JsonResult(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            ApplicationContext db = new ApplicationContext();
            var room = await db.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (room == null)
            {
                return NotFound();
            }
            db.Dispose();
            return new JsonResult(room);
        }

        [HttpPost]
        public async Task<ActionResult> PostRoom(Room roomFromClient)
        {
            if (roomFromClient == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (await db.Rooms.ContainsAsync(roomFromClient))
            {
                return StatusCode(400);
            }
            await db.Rooms.AddAsync(roomFromClient);
            await db.SaveChangesAsync();
            db.Dispose();
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutRoom(Room room)
        {
            if (room == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (!await db.Rooms.ContainsAsync(room))
            {
                return StatusCode(404);
            }
            db.Rooms.Update(room);
            await db.SaveChangesAsync();
            db.Dispose();
            return StatusCode(202);//принято
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRoom(Room roomFromClient)
        {
            if (roomFromClient == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (!await db.Rooms.ContainsAsync(roomFromClient))
            {
                return StatusCode(404);
            }
            db.Rooms.Remove(roomFromClient);
            await db.SaveChangesAsync();
            db.Dispose();
            return StatusCode(202);//принято
        }
    }
}

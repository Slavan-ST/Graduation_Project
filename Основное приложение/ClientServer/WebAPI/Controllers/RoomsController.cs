using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Helper.Data;
using Helper.Models.Main;
using Helper.Models.DTO;
using Helper.Converters;

namespace Helper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            ApplicationContext db = new ApplicationContext();
            var rooms = await db.Rooms.ToListAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            db.Dispose();

            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            foreach (var room in rooms)
            {
                roomDTOs.Add(new RoomDTO(room));
            }

            return new JsonResult(roomDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            ApplicationContext db = new ApplicationContext();
            var room = await db.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (room == null)
            {
                return NotFound();
            }
            db.Dispose();

            RoomDTO roomDTO = new RoomDTO(room);

            return new JsonResult(roomDTO);
        }

        [HttpPost]
        public async Task<ActionResult> PostRoom(RoomDTO? roomDTO)
        {
            if (roomDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            
            //проверка на существование такой записи в БД
            Room? room = await db.Rooms.Where(x => x.Id == roomDTO.Id).FirstOrDefaultAsync();            
            if (room != null)
            {
                return StatusCode(400);
            }

            room = ConverterDTO.RoomFromDTO(roomDTO);

            await db.Rooms.AddAsync(room!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutRoom(RoomDTO? roomDTO)
        {
            if (roomDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();
            //проверка на существование такой записи в БД
            Room? room = await db.Rooms.Where(x => x.Id == roomDTO.Id).FirstOrDefaultAsync();
            if (room == null)
            {
                return StatusCode(404);
            }

            room = ConverterDTO.RoomFromDTO(roomDTO);

            db.Rooms.Update(room!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            ApplicationContext db = new ApplicationContext();

            var room = await db.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (room == null)
            {
                return StatusCode(404);
            }

            db.Rooms.Remove(room);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

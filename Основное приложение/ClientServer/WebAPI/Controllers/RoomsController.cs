using Helper.Converters;
using Helper.Models.DTO;
using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с комнатами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        /// <summary>
        /// Получение всех комнат 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            ApplicationContext db = new ApplicationContext();
            var rooms = await db.Rooms.ToListAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(rooms);
        }
        /// <summary>
        /// Получение комнаты по номеру
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet("{number}")]
        public async Task<ActionResult<Room>> GetRoom(string number)
        {
            ApplicationContext db = new ApplicationContext();
            var room = await db.Rooms
                .Where(x => x.Number == number)
                .FirstOrDefaultAsync();

            if (room == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();


            return new JsonResult(room);
        }
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        /// <param name="roomDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostRoom(Room? roomDTO)
        {
            if (roomDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            
            //проверка на существование такой записи в БД
            Room? room = await db.Rooms
                .Where(x => x.Id == roomDTO.Id)
                .FirstOrDefaultAsync();
            
            if (room != null)
            {
                return StatusCode(400);
            }

            room = roomDTO;

            await db.Rooms.AddAsync(room!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return new JsonResult(room.Id);
        }
        /// <summary>
        /// Изменение комнаты
        /// </summary>
        /// <param name="roomDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> PutRoom(Room? roomDTO)
        {
            if (roomDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();
            //проверка на существование такой записи в БД
            Room? room = await db.Rooms
                .Where(x => x.Id == roomDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (room == null)
            {
                return StatusCode(404);
            }

            room = roomDTO;

            db.Rooms.Update(room!);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
        /// <summary>
        /// Удаление комнаты
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteRoom(string number)
        {
            ApplicationContext db = new ApplicationContext();

            var room = await db.Rooms
                .Where(x => x.Number == number)
                .FirstOrDefaultAsync();

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

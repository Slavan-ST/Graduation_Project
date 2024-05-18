using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Security;
using WebAPI.Data;
using System.Diagnostics;
using Helper.Converters;
using Helper.Models.Main;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с логами журнала наличия студентов в ночное время суток
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AttendanceLogController : ControllerBase
    {
        /// <summary>
        /// Получение всех логов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceLog>>> GetAttendanceLogs()
        {
            ApplicationContext db = new();
            var attendanceLogs = await db.AttendanceLog
                .Include(c => c.Student)               
                .Include(c => c.Student!.Room)
                .ToListAsync();

            if (attendanceLogs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(attendanceLogs);
        }
        /// <summary>
        /// Получение логов за указанный день
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("day:{day}.{month}.{year}")]
        public async Task<ActionResult<IEnumerable<AttendanceLog>>> GetAttendanceLogsDay(int day, int month, int year)
        {
            ApplicationContext db = new();
            var attendanceLogs = await db.AttendanceLog
                .Include(c => c.Student)             
                .Include(c => c.Student!.Room)
                .Where(c => c.Date.Year == year && c.Date.Month == month && c.Date.Day == day)
                .ToListAsync();

            if (attendanceLogs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(attendanceLogs);
        }
        /// <summary>
        /// Получение логов за указанный месяц
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("month:{month}.{year}")]
        public async Task<ActionResult<IEnumerable<AttendanceLog>>> GetAttendanceLogsMonth(int month, int year)
        {
            ApplicationContext db = new();
            var attendanceLogs = await db.AttendanceLog
                .Include(c => c.Student)              
                .Include(c => c.Student!.Room)
                .Where(c => c.Date.Year == year && c.Date.Month == month)
                .ToListAsync();

            if (attendanceLogs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(attendanceLogs);
        }
        /// <summary>
        /// Получение логов за указанный год
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("year:{year}")]
        public async Task<ActionResult<IEnumerable<AttendanceLog>>> GetAttendanceLogsYear(int year)
        {
            ApplicationContext db = new();
            var attendanceLogs = await db.AttendanceLog
                .Include(c => c.Student)              
                .Include(c => c.Student!.Room)
                .Where(c => c.Date.Year == year)
                .ToListAsync();

            if (attendanceLogs == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(attendanceLogs);
        }
        /// <summary>
        /// Получение лога по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceLog>> GetAttendanceLog(int id)
        {
            ApplicationContext db = new();
            var attendanceLog = await db.AttendanceLog
                .Include(c => c.Student)  
                .Include(c => c.Student!.Room)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (attendanceLog == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(attendanceLog);
        }
        /// <summary>
        /// Создание нового лога
        /// </summary>
        /// <param name="attendanceLogDTO">новый лог</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostAttendanceLog(AttendanceLog attendanceLogDTO)
        {
            if (attendanceLogDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new();

            //проверка на существование такой записи в БД
            AttendanceLog? attendanceLog = await db.AttendanceLog
                .Where(x => 
                    x.Date.Year == attendanceLogDTO.Date.Year &&
                    x.Date.Month == attendanceLogDTO.Date.Month &&
                    x.Date.Day == attendanceLogDTO.Date.Day &&
                    x.StudentId == attendanceLogDTO.StudentId)
                .FirstOrDefaultAsync();

            if (attendanceLog != null)
            {
                return StatusCode(409);
            }

            await db.AttendanceLog.AddAsync(attendanceLogDTO);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return new JsonResult(attendanceLogDTO.Id);
        }

        /// <summary>
        /// Обновление указанного лога
        /// </summary>
        /// <param name="attendanceLogDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> PutAttendanceLog(AttendanceLog attendanceLogDTO)
        {
            if (attendanceLogDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new();

            AttendanceLog? attendanceLog = await db.AttendanceLog
                .Where(x =>
                    x.Date.Year == attendanceLogDTO.Date.Year &&
                    x.Date.Month == attendanceLogDTO.Date.Month &&
                    x.Date.Day == attendanceLogDTO.Date.Day &&
                    x.StudentId == attendanceLogDTO.StudentId)
                .FirstOrDefaultAsync();

            if(attendanceLog == null)
            {
                return StatusCode(404);
            }

            attendanceLog.StudentId = attendanceLogDTO.StudentId;
            attendanceLog.Date = attendanceLogDTO.Date;
            attendanceLog.Marker = attendanceLogDTO.Marker;

            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
        /// <summary>
        /// Удаление лога по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendanceLog(int id)
        {
            ApplicationContext db = new();

            var attendanceLog = await db.AttendanceLog
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (attendanceLog == null)
            {
                return StatusCode(404);
            }
            db.AttendanceLog.Remove(attendanceLog);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

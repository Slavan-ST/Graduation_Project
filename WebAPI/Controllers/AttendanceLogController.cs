using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Security;
using WebAPI.Data;
using WebAPI.Models.Main;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceLogController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceLog>>> GetAttendanceLogs()
        {
            ApplicationContext db = new ApplicationContext();
            var attendanceLogs = await db.AttendanceLog.ToListAsync(); 
            if (attendanceLogs == null)
            {
                return NotFound();
            }
            db.Dispose();
            return new JsonResult(attendanceLogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceLog>> GetAttendanceLog(int id)
        {
            ApplicationContext db = new ApplicationContext();
            var attendanceLog = await db.AttendanceLog.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (attendanceLog == null)
            {
                return NotFound();
            }
            db.Dispose();
            return new JsonResult(attendanceLog);
        }

        [HttpPost]
        public async Task<ActionResult> PostAttendanceLog(AttendanceLog attendanceLog)
        {
            if (attendanceLog == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (await db.AttendanceLog.ContainsAsync(attendanceLog))
            {
                return StatusCode(400);
            }
            await db.AttendanceLog.AddAsync(attendanceLog);
            db.Dispose();
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> PutAttendanceLog(AttendanceLog attendanceLog)
        {
            if (attendanceLog == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (!await db.AttendanceLog.ContainsAsync(attendanceLog))
            {
                return StatusCode(404);
            }
            db.AttendanceLog.Update(attendanceLog);
            db.Dispose();
            return StatusCode(202);//принято
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAttendanceLog(AttendanceLog attendanceLog)
        {
            if (attendanceLog == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (!await db.AttendanceLog.ContainsAsync(attendanceLog))
            {
                return StatusCode(404);
            }
            db.AttendanceLog.Remove(attendanceLog);
            db.Dispose();
            return StatusCode(202);//принято
        }
    }
}

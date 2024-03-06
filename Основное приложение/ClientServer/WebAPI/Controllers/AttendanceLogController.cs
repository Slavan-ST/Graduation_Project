using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Helper.Security;
using Helper.Data;
using Helper.Models.Main;
using Helper.Models.DTO;

namespace Helper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceLogController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceLogDTO>>> GetAttendanceLogs()
        {
            ApplicationContext db = new ApplicationContext();
            var attendanceLogs = await db.AttendanceLog.ToListAsync(); 
            if (attendanceLogs == null)
            {
                return NotFound();
            }
            db.Dispose();

            List<AttendanceLogDTO> attendanceLogDTOs = new List<AttendanceLogDTO>();
            foreach (var log in attendanceLogs)
            {
                attendanceLogDTOs.Add(new AttendanceLogDTO(log));
            }

            return new JsonResult(attendanceLogDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceLogDTO>> GetAttendanceLog(int id)
        {
            ApplicationContext db = new ApplicationContext();
            var attendanceLog = await db.AttendanceLog.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (attendanceLog == null)
            {
                return NotFound();
            }
            db.Dispose();

            AttendanceLogDTO attendanceLogDTO = new AttendanceLogDTO(attendanceLog);

            return new JsonResult(attendanceLogDTO);
        }

        [HttpPost]
        public async Task<ActionResult> PostAttendanceLog(AttendanceLogDTO attendanceLogDTO)
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

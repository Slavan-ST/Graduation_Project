using Helper.Converters;
using Helper.Models.DTO;
using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            ApplicationContext db = new ApplicationContext();
            var students = await db.Students
                .Include(c => c.Room)
                .Include(c => c.AttendanceLogs)
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(students);
        }
        [HttpGet("{room}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsFromRoom(string room)
        {
            ApplicationContext db = new ApplicationContext();

            var students = await db.Students
                .Include(c => c.Room)
                .Include(c => c.AttendanceLogs)
                .Where(x => x.Room!.Number == room)
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(students);
        }

        [HttpGet("{room}/{fio}")]
        public async Task<ActionResult<Student>> GetStudent(string room, string fio)
        {
            room = room.Trim();
            fio = fio.Trim();

            (string surname, string name, string? patronymic) = FIOConverter.GetSurnameNamePatronymicFromFIO(fio);
            
            ApplicationContext db = new ApplicationContext();
            var student = await db.Students
                .Include(c => c.Room)
                .Include(c => c.AttendanceLogs)
                .Where(x => x.Room!.Number == room && x.Surname == surname && x.Name == name && x.Patronymic == patronymic)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                Debug.WriteLine($"{room}/{fio}");
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(student);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Student studentDTO)
        {
            if (studentDTO == null)
            {
                return NoContent();
            }

            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            Student? student = await db.Students
                .Include(c => c.Room)
                .Where(x => x.Id == studentDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (student != null)
            {
                return StatusCode(400);
            }

            student = studentDTO!;

            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Student studentDTO)
        {
            if (studentDTO == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();

            //проверка на существование такой записи в БД
            Student? student = await db.Students
                .Where(x => x.Id == studentDTO.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return StatusCode(404);
            }

            student = studentDTO!;

            db.Students.Update(student);

            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }

        [HttpDelete("{room}/{fio}")]
        public async Task<ActionResult> Delete(string room, string fio)
        {
            room = room.Trim();
            fio = fio.Trim();

            (string surname, string name, string? patronymic) = FIOConverter.GetSurnameNamePatronymicFromFIO(fio);

            ApplicationContext db = new ApplicationContext();
            var student = await db.Students
                .Include(c => c.Room)
                .Where(x => x.Room!.Number == room && x.Surname == surname && x.Name == name && x.Patronymic == patronymic)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return StatusCode(404);
            }

            db.Students.Remove(student);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(202);//принято
        }
    }
}

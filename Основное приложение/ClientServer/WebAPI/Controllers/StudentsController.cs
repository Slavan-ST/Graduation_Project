using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Helper.Data;
using Helper.Models.Main;
using Helper.Models.DTO;
using Helper.Converters;
using WebAPI.Models;

namespace Helper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            ApplicationContext db = new ApplicationContext();
            var students = await db.Students.Include(c => c.Room).ToListAsync();
            if (students == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            foreach (var student in students)
            {
                studentDTOs.Add(new StudentDTO(student));
            }

            return new JsonResult(studentDTOs);
        }
        [HttpGet("{room}")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsFromRoom(string room)
        {
            ApplicationContext db = new ApplicationContext();
            var students = await db.Students.Include(c => c.Room).Where(x => x.Room!.Number == room).ToListAsync();
            if (students == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            foreach (var student in students)
            {
                studentDTOs.Add(new StudentDTO(student));
            }

            return new JsonResult(studentDTOs);
        }

        [HttpGet("{room}/{fio}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(string room, string fio)
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
                Debug.WriteLine($"{room}/{fio}");
                return NotFound();
            }
            await db.DisposeAsync();

            StudentDTO studentDTO = new StudentDTO(student);

            return new JsonResult(studentDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post(StudentDTO studentDTO)
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

            student = ConverterDTO.StudentFromDTO(studentDTO)!;

            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();
            await db.DisposeAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> Put(StudentDTO studentDTO)
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

            student = ConverterDTO.StudentFromDTO(studentDTO)!;

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

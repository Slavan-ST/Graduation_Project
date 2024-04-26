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
    /// <summary>
    /// Контроллер для работы со студентами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        /// <summary>
        /// Получение всех студентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            ApplicationContext db = new ApplicationContext();
            var students = await db.Students
                .Include(c => c.Room)
                .Include(c => c.Status)
                .Include(c => c.AttendanceLogs)
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }
            await db.DisposeAsync();

            return new JsonResult(students);
        }
        /// <summary>
        /// Получение студентов по указанной комнате
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpGet("{room}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsFromRoom(string room)
        {
            ApplicationContext db = new ApplicationContext();

            var students = await db.Students
                .Include(c => c.Room)
                .Include(c => c.Status)
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
        /// <summary>
        /// Получение студента по комнате и ФИО
        /// </summary>
        /// <param name="room"></param>
        /// <param name="fio"></param>
        /// <returns></returns>
        [HttpGet("{room}/{fio}")]
        public async Task<ActionResult<Student>> GetStudent(string room, string fio)
        {
            room = room.Trim();
            fio = fio.Trim();

            (string surname, string name, string? patronymic) = FIOConverter.GetSurnameNamePatronymicFromFIO(fio);
            
            ApplicationContext db = new ApplicationContext();
            var student = await db.Students
                .Include(c => c.Room)
                .Include(c => c.Status)
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

        /// <summary>
        /// Добавление нового студента
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
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
                .Include(c => c.Status)
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
        /// <summary>
        /// Изменение студента
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Удаление студента
        /// </summary>
        /// <param name="room"></param>
        /// <param name="fio"></param>
        /// <returns></returns>
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

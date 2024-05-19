using Helper.Converters;
using Helper.Models.DTO;
using Helper.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            ApplicationContext db = new();
            var students = await db.Students
                .Include(c => c.Room)
                .Include(c => c.Status)
                .Include(c => c.Group)
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
            ApplicationContext db = new();

            var students = await db.Students
                .Include(c => c.Room)
                .Include(c => c.Status)
                .Include(c => c.AttendanceLogs)
                .Include(c => c.Group)
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
            
            ApplicationContext db = new();
            var student = await db.Students
                .Include(c => c.Room)
                .Include(c => c.Status)
                .Include(c => c.AttendanceLogs)
                .Include(c => c.Group)
                .Where(x => x.Room!.Number == room && x.Surname == surname && x.Name == name && x.Patronymic == patronymic)
                .FirstOrDefaultAsync();

            if (student == null)
            {
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
            try
            {
                if (studentDTO == null)
                {
                    return NoContent();
                }


                ApplicationContext db = new();

                //проверка на существование такой записи в БД
                //Варианта 2:
                //1) Совпал Id - такого быть не должно
                //2) Совпало ФИО и номер телефона студентов, такого тоже быть не должно

                Student? student = await db.Students
                    .Where(x => x.Id == studentDTO.Id ||
                                x.Phone == studentDTO.Phone &&
                                x.Name == studentDTO.Name &&
                                x.Surname == studentDTO.Surname &&
                                x.Patronymic == studentDTO.Patronymic
                           )
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (student != null)
                {
                    return StatusCode(409);
                }

                if (studentDTO.Status != null && studentDTO.Group != null && studentDTO.Room != null)
                {
                    studentDTO.StatusId = studentDTO.Status.Id;
                    studentDTO.GroupId = studentDTO.Group.Id;
                    studentDTO.RoomId = studentDTO.Room.Id;
                }

                if (studentDTO.DateBirthday.Year < 1754)
                {
                    studentDTO.DateBirthday = new DateTime(1754,1,1);
                }
                await db.Database.ExecuteSqlRawAsync(
                    @"insert into Students values 
                     (@name, @sname, @pat, @phone, @gender,@adr,@date, @repn, @reps, @repp, @repph, @sid,@gid,@rid);",

                    new SqlParameter("@name", studentDTO.Name),
                    new SqlParameter("@sname", studentDTO.Surname),
                    new SqlParameter("@pat", studentDTO.Patronymic),
                    new SqlParameter("@phone", studentDTO.Phone),
                    new SqlParameter("@gender", studentDTO.Gender),
                    new SqlParameter("@adr", studentDTO.Address),
                    new SqlParameter("@date", studentDTO.DateBirthday),
                    new SqlParameter("@repn", studentDTO.RepresentativeName),
                    new SqlParameter("@reps", studentDTO.RepresentativeSurname),
                    new SqlParameter("@repp", studentDTO.RepresentativePatronymic),
                    new SqlParameter("@repph", studentDTO.RepresentativePhone),
                    new SqlParameter("@sid", studentDTO.StatusId),
                    new SqlParameter("@gid", studentDTO.GroupId),
                    new SqlParameter("@rid", studentDTO.RoomId)

                    );

                //await db.Students.AddAsync(studentDTO);
                await db.SaveChangesAsync();
                await db.DisposeAsync();

                return new JsonResult(studentDTO.Id);
            }
            catch
            {
                return StatusCode(400);
            }
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
            ApplicationContext db = new();

            //проверка на существование такой записи в БД
            Student? student = await db.Students
                .Include(x => x.AttendanceLogs)
                .Include(x => x.DutySchedules)
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ApplicationContext db = new();
            var student = await db.Students
                .Include(x => x.AttendanceLogs)
                 //.Include(x => x.Duty)
                .Where(x => x.Id == id)
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

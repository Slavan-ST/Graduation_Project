using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.Data;

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
            var students = await db.Students.ToListAsync();
            return new JsonResult(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            ApplicationContext db = new ApplicationContext();
            var student = await db.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }
            db.Dispose();
            return new JsonResult(student);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Student student)
        {
            if (student == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (await db.Students.ContainsAsync(student))
            {
                return StatusCode(400);
            }
            await db.Students.AddAsync(student);
            db.Dispose();
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Student student)
        {
            if (student == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (!await db.Students.ContainsAsync(student))
            {
                return StatusCode(404);
            }
            db.Students.Update(student);
            db.Dispose();
            return StatusCode(202);//принято
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Student student)
        {
            if (student == null)
            {
                return NoContent();
            }
            ApplicationContext db = new ApplicationContext();
            if (!await db.Students.ContainsAsync(student))
            {
                return StatusCode(404);
            }
            db.Students.Remove(student);
            db.Dispose();
            return StatusCode(202);//принято
        }
    }
}

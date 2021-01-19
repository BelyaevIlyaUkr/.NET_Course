using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudyManager.Models;
using StudyManager.DataAccess.ADO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        IConfiguration Configuration { get; }

        public StudentsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // GET: api/<StudentsController>
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                return await StudentsRepository.GetAllStudentsAsync(connection);
            } 
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            if (id == 0)
                return BadRequest("Error: student id can't be zero and must be filled");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var student = await StudentsRepository.GetDefiniteStudent(connection, id);

                if (student == null)
                    return NotFound("There isn't student with such ID in database");

                return new ObjectResult(student);
            }
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Error: student object can't be null");

            if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName)
                || string.IsNullOrEmpty(student.PhoneNumber) || string.IsNullOrEmpty(student.Github)
                || string.IsNullOrEmpty(student.Email))
            {
                return BadRequest("Error: all input fields must be filled");
            }

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                await StudentsRepository.CreateStudentAsync(connection, student); 
            }

            return Ok();
        }

        // PUT api/<StudentsController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Error: student object can't be null");

            if (student.StudentID <= 0)
                return BadRequest("Error: student ID can't be zero or less and must be filled");

            if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName) 
                || string.IsNullOrEmpty(student.PhoneNumber) || string.IsNullOrEmpty(student.Github) 
                || string.IsNullOrEmpty(student.Email))
            {
                return BadRequest("Error: all input fields must be filled");
            }

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await StudentsRepository.UpdateStudentAsync(connection, student);

                if (numberOfAffectedRows == 0)
                    return NotFound("Student with such id isn't found in database");
            }

            return Ok();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Error: student id can't be negative or zero and must be filled");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await StudentsRepository.DeleteStudentAsync(connection,id);

                if (numberOfAffectedRows == 0)
                    return NotFound("Student with such id isn't found in database");
            }

            return Ok();
        }

        [HttpDelete]
        public async Task Delete()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                await StudentsRepository.DeleteAllStudentsAsync(connection);
            }
        }
    }
}

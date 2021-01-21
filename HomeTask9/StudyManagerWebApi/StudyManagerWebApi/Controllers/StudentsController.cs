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
            if (Validation.IsAnyFieldNotFilled(new List<object> { id }))
                return BadRequest("Error: student ID must be filled (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var student = await StudentsRepository.GetDefiniteStudent(connection, id);

                if (student == null)
                    return NotFound("There isn't student with such id in database");

                return new ObjectResult(student);
            }
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> student)
        {
            if (student == null)
                return BadRequest("Error: student object can't be null");

            if (Validation.IsAnyFieldNotFilled(new List<object> { student.ContainsKey("FirstName") ? student["FirstName"] : null,
                student.ContainsKey("LastName") ? student["LastName"] : null, student.ContainsKey("PhoneNumber") ? 
                student["PhoneNumber"] : null, student.ContainsKey("Email") ? student["Email"] : null, 
                student.ContainsKey("Github") ? student["Github"] : null}))
            {
                return BadRequest("Error: all student input fields must be filled");
            }

            if (!Validation.IsValidFirstOrLastName(student["FirstName"]))
                return BadRequest("Error: first name is incorrect");

            if (!Validation.IsValidFirstOrLastName(student["LastName"]))
                return BadRequest("Error: last name is incorrect");

            if (!Validation.IsValidPhoneNumber(student["PhoneNumber"]))
                return BadRequest("Error: phone number is incorrect");

            if (!Validation.IsValidEmailAddress(student["Email"]))
                return BadRequest("Error: email address is incorrect");

            var resultStudent = new Student
            {
                FirstName = student["FirstName"],
                LastName = student["LastName"],
                PhoneNumber = student["PhoneNumber"],
                Email = student["Email"],
                Github = student["Github"]
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                await StudentsRepository.CreateStudentAsync(connection, resultStudent); 
            }

            return Ok(resultStudent);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dictionary<string, string> student)
        {
            if (student == null)
                return BadRequest("Error: student object can't be null");

            if (Validation.IsAnyFieldNotFilled(new List<object> { id, student.ContainsKey("FirstName") ? student["FirstName"] : null,
                student.ContainsKey("LastName") ? student["LastName"] : null, student.ContainsKey("PhoneNumber") ?
                student["PhoneNumber"] : null, student.ContainsKey("Email") ? student["Email"] : null,
                student.ContainsKey("Github") ? student["Github"] : null}))
            {
                return BadRequest("Error: all student input fields must be filled (student ID - with non-zero value)");
            }

            student["FirstName"] = student["FirstName"].Trim();
            student["LastName"] = student["LastName"].Trim();
            student["PhoneNumber"] = student["PhoneNumber"].Trim();
            student["Email"] = student["Email"].Trim();
            student["Github"] = student["Github"].Trim();

            if (!Validation.IsValidFirstOrLastName(student["FirstName"]))
                return BadRequest("Error: first name is incorrect");

            if (!Validation.IsValidFirstOrLastName(student["LastName"]))
                return BadRequest("Error: last name is incorrect");

            if (!Validation.IsValidPhoneNumber(student["PhoneNumber"]))
                return BadRequest("Error: phone number is incorrect");

            if (!Validation.IsValidEmailAddress(student["Email"]))
                return BadRequest("Error: email address is incorrect");

            var resultStudent = new Student
            {
                StudentID = id,
                FirstName = student["FirstName"],
                LastName = student["LastName"],
                PhoneNumber = student["PhoneNumber"],
                Email = student["Email"],
                Github = student["Github"]
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await StudentsRepository.UpdateStudentAsync(connection, resultStudent);

                if (numberOfAffectedRows == 0)
                    return NotFound("Student with such id isn't found in database");
            }

            return NoContent();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (Validation.IsAnyFieldNotFilled(new List<object> { id }))
                return BadRequest("Error: student ID must be filled (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await StudentsRepository.DeleteStudentAsync(connection,id);

                if (numberOfAffectedRows == 0)
                    return NotFound("Student with such id isn't found in database");
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await StudentsRepository.DeleteAllStudentsAsync(connection);

                if (numberOfAffectedRows == 0)
                    return NotFound("There aren't any students in database");
            }

            return NoContent();
        }
    }
}

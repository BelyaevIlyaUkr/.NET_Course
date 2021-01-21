using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudyManager.Models;
using StudyManager.DataAccess.ADO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        IConfiguration Configuration { get; }

        public LecturersController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<LecturersController>
        [HttpGet]
        public async Task<List<Lecturer>> GetAllLecturers()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                return await LecturersRepository.GetAllLecturersAsync(connection);
            }
        }

        // GET api/<LecturersController>/5
        [HttpGet("{lecturerID}")]
        public async Task<ActionResult<Lecturer>> GetLecturerWithID(int lecturerID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { lecturerID }))
                return BadRequest("Error: lecturer ID must be filled (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var lecturer = await LecturersRepository.GetDefiniteLecturer(connection, lecturerID);

                if (lecturer == null)
                    return NotFound("There isn't lecturer with such ID in database");

                return new ObjectResult(lecturer);
            }
        }

        // POST api/<LecturersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> lecturer)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { lecturer.ContainsKey("Name") ? lecturer["Name"] : null,
                lecturer.ContainsKey("BirthDate") ? lecturer["BirthDate"] : null}))
            {
                return BadRequest("Error: all lecturer input data must be specified");
            }

            lecturer["Name"] = lecturer["Name"].Trim();
            lecturer["BirthDate"] = lecturer["BirthDate"].Trim();

            if (!Validation.IsValidFirstOrLastName(lecturer["Name"]))
                return BadRequest("Error: name is incorrect");

            if (!Validation.ValidateDateTimeAndGetParsed(lecturer["BirthDate"], out DateTime resultBirthDate))
                return BadRequest("Error: birth date is incorrect");

            var resultLecturer = new Lecturer
            {
                Name = lecturer["Name"],
                BirthDate = resultBirthDate
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                await LecturersRepository.CreateLecturerAsync(connection, resultLecturer);
            }

            return Ok(resultLecturer);
        }

        // PUT api/<LecturersController>/5
        [HttpPut("{lecturerID}")]
        public async Task<IActionResult> Put(int lecturerID, [FromBody] Dictionary<string, string> lecturer)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { lecturerID, lecturer.ContainsKey("Name") ? lecturer["Name"] : null,
                lecturer.ContainsKey("BirthDate") ? lecturer["BirthDate"] : null}))
            {
                return BadRequest("Error: all lecturer input data must be specified");
            }

            lecturer["Name"] = lecturer["Name"].Trim();
            lecturer["BirthDate"] = lecturer["BirthDate"].Trim();

            if (!Validation.IsValidFirstOrLastName(lecturer["Name"]))
                return BadRequest("Error: name is incorrect");

            if (!Validation.ValidateDateTimeAndGetParsed(lecturer["BirthDate"], out DateTime resultBirthDate))
                return BadRequest("Error: birth date is incorrect");

            var resultLecturer = new Lecturer
            {
                LecturerID = lecturerID,
                Name = lecturer["Name"],
                BirthDate = resultBirthDate
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                await LecturersRepository.UpdateLecturerAsync(connection, resultLecturer);
            }

            return NoContent();
        }

        // DELETE api/<LecturersController>/5
        [HttpDelete("{lecturerID}")]
        public async Task<IActionResult> DeleteLecturerWithID(int lecturerID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { lecturerID }))
                return BadRequest("Error: lecturer ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await LecturersRepository.DeleteLecturerAsync(connection, lecturerID);

                if (numberOfAffectedRows == 0)
                    return NotFound("Lecturer with such id isn't found in database");
            }

            return NoContent();
        }

        // DELETE api/<LecturersController>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllLecturers()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await LecturersRepository.DeleteAllLecturersAsync(connection);

                if (numberOfAffectedRows == 0)
                    return NotFound("There aren't any lecturers in database");
            }

            return NoContent();
        }
    }
}

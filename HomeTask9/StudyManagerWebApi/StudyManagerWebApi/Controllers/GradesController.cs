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
    public class GradesController : ControllerBase
    {
        IConfiguration Configuration { get; }

        public GradesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<GradesController>
        [HttpGet]
        public async Task<ActionResult<List<Grade>>> GetAllGrades()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    return await GradesRepository.GetAllGradesAsync(connection);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // GET api/<GradesController>/5
        [HttpGet("{gradeID}")]
        public async Task<ActionResult<Grade>> GetGradeWithID(int gradeID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { gradeID }))
                return BadRequest("Error: grade ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var grade = await GradesRepository.GetDefiniteGrade(connection, gradeID);

                    if (grade == null)
                        return NotFound("There isn't grade with such ID in database");

                    return grade;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // POST api/<GradesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> grade)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { grade.ContainsKey("GradeDate") ? grade["GradeDate"] : null,
                grade.ContainsKey("IsComplete") ? grade["IsComplete"] : null, grade.ContainsKey("HomeTaskID") ? grade["HomeTaskID"] : null,
                grade.ContainsKey("StudentID") ? grade["StudentID"] : null}))
            {
                return BadRequest("Error: all grade input data must be specified");
            }

            grade["GradeDate"] = grade["GradeDate"].Trim();
            grade["IsComplete"] = grade["IsComplete"].Trim();
            grade["HomeTaskID"] = grade["HomeTaskID"].Trim();
            grade["StudentID"] = grade["StudentID"].Trim();

            if (!Validation.ValidateDateTimeAndGetParsed(grade["GradeDate"], out DateTime resultGradeDate))
                return BadRequest("Error: grade date is incorrect");

            if (!Validation.ValidateBoolAndGetParsed(grade["IsComplete"], out bool resultIsComplete))
                return BadRequest("Error: is complete is incorrect");

            if (!Validation.ValidateIntAndGetParsed(grade["HomeTaskID"], out int resultHomeTaskID))
                return BadRequest("Error: hometask ID is incorrect");

            if (!Validation.ValidateIntAndGetParsed(grade["StudentID"], out int resultStudentID))
                return BadRequest("Error: student ID is incorrect");

            var resultGrade = new Grade
            {
                GradeDate = resultGradeDate,
                IsComplete = resultIsComplete,
                HomeTaskID = resultHomeTaskID,
                StudentID = resultStudentID
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    await GradesRepository.CreateGradeAsync(connection, resultGrade);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(resultGrade);
        }

        // PUT api/<GradesController>/5
        [HttpPut("{gradeID}")]
        public async Task<IActionResult> Put(int gradeID, [FromBody] Dictionary<string, string> grade)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { gradeID, grade.ContainsKey("GradeDate") ? grade["GradeDate"] : null,
                grade.ContainsKey("IsComplete") ? grade["IsComplete"] : null, grade.ContainsKey("HomeTaskID") ? grade["HomeTaskID"] : null,
                grade.ContainsKey("StudentID") ? grade["StudentID"] : null}))
            {
                return BadRequest("Error: all grade input data must be specified");
            }

            grade["GradeDate"] = grade["GradeDate"].Trim();
            grade["IsComplete"] = grade["IsComplete"].Trim();
            grade["HomeTaskID"] = grade["HomeTaskID"].Trim();
            grade["StudentID"] = grade["StudentID"].Trim();

            if (!Validation.ValidateDateTimeAndGetParsed(grade["GradeDate"], out DateTime resultGradeDate))
                return BadRequest("Error: grade date is incorrect");

            if (!Validation.ValidateBoolAndGetParsed(grade["IsComplete"], out bool resultIsComplete))
                return BadRequest("Error: is complete is incorrect");

            if (!Validation.ValidateIntAndGetParsed(grade["HomeTaskID"], out int resultHomeTaskID))
                return BadRequest("Error: hometask ID is incorrect");

            if (!Validation.ValidateIntAndGetParsed(grade["StudentID"], out int resultStudentID))
                return BadRequest("Error: student ID is incorrect");

            var resultGrade = new Grade
            {
                GradeID = gradeID,
                GradeDate = resultGradeDate,
                IsComplete = resultIsComplete,
                HomeTaskID = resultHomeTaskID,
                StudentID = resultStudentID
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    await GradesRepository.UpdateGradeAsync(connection, resultGrade);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE api/<GradesController>/5
        [HttpDelete("{gradeID}")]
        public async Task<IActionResult> DeleteGradeWithID(int gradeID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { gradeID }))
                return BadRequest("Error: grade ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await GradesRepository.DeleteGradeAsync(connection, gradeID);

                    if (numberOfAffectedRows == 0)
                        return NotFound("Grade with such id isn't found in database");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE api/<GradesController>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllGrades()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await GradesRepository.DeleteAllGradesAsync(connection);

                    if (numberOfAffectedRows == 0)
                        return NotFound("There aren't any grades in database");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }
    }
}

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
    public class CoursesController : ControllerBase
    {
        IConfiguration Configuration { get; }

        public CoursesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    return await CoursesRepository.GetAllCoursesAsync(connection);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // GET api/<CoursesController>/5
        [HttpGet("{courseID}")]
        public async Task<ActionResult<Course>> GetCourseWithID(int courseID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { courseID }))
                return BadRequest("Error: course ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var course = await CoursesRepository.GetDefiniteCourse(connection, courseID);

                    if (course == null)
                        return NotFound("There isn't course with such ID in database");

                    return course;
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> course)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { course.ContainsKey("Name") ? course["Name"] : null, 
                course.ContainsKey("StartDate") ? course["StartDate"] : null, course.ContainsKey("EndDate") ? course["EndDate"] : null, 
                course.ContainsKey("PassingScore") ? course["PassingScore"] : null}))
            {
                return BadRequest("Error: all course input data must be specified");
            }

            course["Name"] = course["Name"].Trim();
            course["StartDate"] = course["StartDate"].Trim();
            course["EndDate"] = course["EndDate"].Trim();
            course["PassingScore"] = course["PassingScore"].Trim();

            if (!Validation.ValidateDateTimeAndGetParsed(course["StartDate"], out DateTime resultStartDate))
                return BadRequest("Error: start date is incorrect");

            if (!Validation.ValidateDateTimeAndGetParsed(course["EndDate"], out DateTime resultEndDate))
                return BadRequest("Error: end date is incorrect");

            if (!Validation.ValidateIntAndGetParsed(course["PassingScore"], out int resultPassingScore) || resultPassingScore <= 0)
                return BadRequest("Error: passing score is incorrect");

            var resultCourse = new Course
            {
                Name = course["Name"],
                StartDate = resultStartDate,
                EndDate = resultEndDate,
                PassingScore = resultPassingScore
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    await CoursesRepository.CreateCourseAsync(connection, resultCourse);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(resultCourse);
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{courseID}")]
        public async Task<IActionResult> Put(int courseID, [FromBody] Dictionary<string, string> course)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { courseID, course.ContainsKey("Name") ? course["Name"] : null,
                course.ContainsKey("StartDate") ? course["StartDate"] : null, course.ContainsKey("EndDate") ? course["EndDate"] : null,
                course.ContainsKey("PassingScore") ? course["PassingScore"] : null}))
            {
                return BadRequest("Error: all course input data must be specified (course ID - with non-zero value)");
            }

            course["Name"] = course["Name"].Trim();
            course["StartDate"] = course["StartDate"].Trim();
            course["EndDate"] = course["EndDate"].Trim();
            course["PassingScore"] = course["PassingScore"].Trim();

            if (!Validation.ValidateDateTimeAndGetParsed(course["StartDate"], out DateTime resultStartDate))
                return BadRequest("Error: start date is incorrect");

            if (!Validation.ValidateDateTimeAndGetParsed(course["EndDate"], out DateTime resultEndDate))
                return BadRequest("Error: end date is incorrect");

            if (!Validation.ValidateIntAndGetParsed(course["PassingScore"], out int resultPassingScore) || resultPassingScore <= 0)
                return BadRequest("Error: passing score is incorrect");

            var resultCourse = new Course
            {
                CourseID = courseID,
                Name = course["Name"],
                StartDate = resultStartDate,
                EndDate = resultEndDate,
                PassingScore = resultPassingScore
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await CoursesRepository.UpdateCourseAsync(connection, resultCourse);

                    if (numberOfAffectedRows == 0)
                        return NotFound("Course with such id isn't found in database");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{courseID}")]
        public async Task<IActionResult> DeleteCourseWithID(int courseID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { courseID }))
                return BadRequest("Error: course ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await CoursesRepository.DeleteCourseAsync(connection, courseID);

                    if (numberOfAffectedRows == 0)
                        return NotFound("Course with such id isn't found in database");
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }


        // DELETE api/<CoursesController>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllCourses()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await CoursesRepository.DeleteAllCoursesAsync(connection);

                    if (numberOfAffectedRows == 0)
                        return NotFound("There aren't any courses in database");
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

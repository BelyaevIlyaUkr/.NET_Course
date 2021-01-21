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
        public async Task<IEnumerable<Course>> Get()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                return await CoursesRepository.GetAllCoursesAsync(connection);
            }
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id)
        {
            if (Validation.IsAnyFieldNotFilled(new List<object> { id }))
                return BadRequest("Error: course ID must be filled (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var course = await CoursesRepository.GetDefiniteCourse(connection, id);

                if (course == null)
                    return NotFound("There isn't course with such ID in database");

                return new ObjectResult(course);
            }
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> course)
        {
            if (course == default)
                return BadRequest("Error: course object can't be empty");

            if (Validation.IsAnyFieldNotFilled(new List<object> { course.ContainsKey("Name") ? course["Name"] : null, 
                course.ContainsKey("StartDate") ? course["StartDate"] : null, course.ContainsKey("EndDate") ? course["EndDate"] : null, 
                course.ContainsKey("PassingScore") ? course["PassingScore"] : null}))
            {
                return BadRequest("Error: all fields must be filled");
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
                connection.Open();

                await CoursesRepository.CreateCourseAsync(connection, resultCourse);
            }

            return Ok(resultCourse);
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dictionary<string, string> course)
        {
            if (course == default)
                return BadRequest("Error: course object can't be empty");

            if (Validation.IsAnyFieldNotFilled(new List<object> { id, course.ContainsKey("Name") ? course["Name"] : null,
                course.ContainsKey("StartDate") ? course["StartDate"] : null, course.ContainsKey("EndDate") ? course["EndDate"] : null,
                course.ContainsKey("PassingScore") ? course["PassingScore"] : null}))
            {
                return BadRequest("Error: all fields must be filled (course ID with non-zero value)");
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
                CourseID = id,
                Name = course["Name"],
                StartDate = resultStartDate,
                EndDate = resultEndDate,
                PassingScore = resultPassingScore
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await CoursesRepository.UpdateCourseAsync(connection, resultCourse);

                if (numberOfAffectedRows == 0)
                    return NotFound("Course with such id isn't found in database");
            }

            return NoContent();
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (Validation.IsAnyFieldNotFilled(new List<object> { id }))
                return BadRequest("Error: course ID must be filled (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await CoursesRepository.DeleteCourseAsync(connection, id);

                if (numberOfAffectedRows == 0)
                    return NotFound("Course with such id isn't found in database");
            }

            return NoContent();
        }


        // DELETE api/<CoursesController>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var numberOfAffectedRows = await CoursesRepository.DeleteAllCoursesAsync(connection);

                if (numberOfAffectedRows == 0)
                    return NotFound("There aren't any courses in database");
            }

            return NoContent();
        }
    }
}

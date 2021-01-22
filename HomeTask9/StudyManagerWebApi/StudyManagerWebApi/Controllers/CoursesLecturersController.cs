using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudyManager.Models;
using StudyManager.DataAccess.ADO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesLecturersController : ControllerBase
    {
        IConfiguration Configuration { get; }

        public CoursesLecturersController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<CoursesLecturersController>
        [HttpGet]
        public async Task<ActionResult<List<Dictionary<string, int>>>> GetAllCoursesLecturers()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    return await CoursesLecturersRepository.GetAllCoursesLecturersAsync(connection);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // GET: api/<CoursesLecturersController>/getAllLecturersForCourse?courseID=5
        [HttpGet]
        [Route("getAllLecturersForCourse")]
        public async Task<ActionResult<List<Lecturer>>> GetAllLecturersForCourse(int courseID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { courseID }))
                return BadRequest("Error: course ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var lecturersForCourse = await CoursesLecturersRepository.GetAllLecturersForCourseAsync(connection, courseID);

                    if (lecturersForCourse.Count == 0)
                        return NotFound("There aren't any lecturers on this course");

                    return lecturersForCourse;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


        // GET: api/<CoursesLecturersController>/getAllCoursesWithDefiniteLecturer?lecturerID=8
        [HttpGet]
        [Route("getAllCoursesWithDefiniteLecturer")]
        public async Task<ActionResult<List<Course>>> GetAllCoursesWithDefiniteLecturer(int lecturerID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { lecturerID }))
                return BadRequest("Error: lecturer ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var coursesWithDefiniteLecturer = await CoursesLecturersRepository.GetAllCoursesWithDefiniteLecturerAsync(connection, lecturerID);

                    if (coursesWithDefiniteLecturer.Count == 0)
                        return NotFound("There aren't any courses with this lecturer");

                    return coursesWithDefiniteLecturer;
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // POST api/<CoursesLecturersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, int> courseLecturer)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { courseLecturer.ContainsKey("CourseID") ?
                courseLecturer["CourseID"] : 0, courseLecturer.ContainsKey("LecturerID") ? courseLecturer["LecturerID"] : 0}))
            {
                return BadRequest("Error: both course and lecturer IDs must be specified (with non-zero value)");
            }

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                try
                {
                    await CoursesLecturersRepository.CreateCourseLecturerAsync(connection, 
                        (courseLecturer["CourseID"], courseLecturer["LecturerID"]));
                }
                catch (SqlException)
                {
                    return BadRequest("Error: there isn't lecturer or/and course with such ID or " +
                        "this lecturer have already been connected to this course");
                }
                catch (Exception)
                {
                    return BadRequest("Error: something went wrong");
                }
            }

            return Ok(courseLecturer);
        }


        // DELETE api/<CoursesLecturersController>/definiteCourseLecturer
        [HttpDelete]
        [Route("definiteCourseLecturer")]
        public async Task<IActionResult> DeleteCourseLecturer([FromBody] Dictionary<string, int> courseLecturer)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { courseLecturer.ContainsKey("CourseID") ?
                courseLecturer["CourseID"] : 0, courseLecturer.ContainsKey("LecturerID") ? courseLecturer["LecturerID"] : 0}))
            {
                return BadRequest("Error: both course and lecturer IDs must be specified (with non-zero value)");
            }

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try {
                    connection.Open();

                    var numberOfAffectedRows = await CoursesLecturersRepository.DeleteCourseLecturerAsync(connection,
                        (courseLecturer["CourseID"], courseLecturer["LecturerID"]));

                    if (numberOfAffectedRows == 0)
                        return NotFound("Lecturer with this lecturer ID doesn't teach on course with this course ID");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE api/<CoursesLecturersController>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllCoursesLecturers()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await CoursesLecturersRepository.DeleteAllCoursesLecturersAsync(connection);

                    if (numberOfAffectedRows == 0)
                        return NotFound("There aren't any lecturers on any courses");
                }
                catch(Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return NoContent();
        }
    }
}

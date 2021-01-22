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
    public class StudentsCoursesController : ControllerBase
    {
        IConfiguration Configuration { get; }

        public StudentsCoursesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<StudentsCoursesController>/getAllCoursesForStudent?studentID=5
        [HttpGet]
        [Route("getAllCoursesForStudent")]
        public async Task<ActionResult<List<Course>>> GetAllCoursesForStudent(int studentID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { studentID }))
                return BadRequest("Error: student ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var coursesForStudent = await StudentsCoursesRepository.GetAllCoursesForStudentAsync(connection, studentID);

                    if (coursesForStudent.Count == 0)
                        return NotFound("This student doesn't study any course");

                    return new ObjectResult(coursesForStudent);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // GET: api/<StudentsCoursesController>/getAllStudentsOnCourse?courseID=8
        [HttpGet]
        [Route("getAllStudentsOnCourse")]
        public async Task<ActionResult<List<Course>>> GetAllStudentsOnCourse(int courseID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { courseID }))
                return BadRequest("Error: course ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var studentsOnCourse = await StudentsCoursesRepository.GetAllStudentsInCourseAsync(connection, courseID);

                    if (studentsOnCourse.Count == 0)
                        return NotFound("There aren't any students on this course");

                    return new ObjectResult(studentsOnCourse);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // GET: api/<StudentsCoursesController>
        [HttpGet]
        public async Task<ActionResult<List<Dictionary<string, int>>>> GetAllStudentsCourses()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    return await StudentsCoursesRepository.GetAllStudentsCoursesAsync(connection);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // POST api/<StudentsCoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, int> studentCourse)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { studentCourse.ContainsKey("StudentID") ? 
                studentCourse["StudentID"] : 0, studentCourse.ContainsKey("CourseID") ? studentCourse["CourseID"] : 0}))
            {
                return BadRequest("Error: both student and course IDs must be specified (with non-zero value)");
            }

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    await StudentsCoursesRepository.CreateStudentCourseAsync(connection, (studentCourse["StudentID"], studentCourse["CourseID"]));
                }
                catch(SqlException)
                {
                    return BadRequest("Error: there isn't student or/and course with such ID or " +
                        "this student have already been connected to this course");
                }
                catch (Exception)
                {
                    return BadRequest("Error: something went wrong");
                }
            }

            return Ok(studentCourse);
        }

        // DELETE api/<StudentsCoursesController>/definiteStudentCourse
        [HttpDelete]
        [Route("definiteStudentCourse")]
        public async Task<IActionResult> DeleteStudentCourse([FromBody] Dictionary<string, int> studentCourse)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { studentCourse.ContainsKey("StudentID") ?
                studentCourse["StudentID"] : 0, studentCourse.ContainsKey("CourseID") ? studentCourse["CourseID"] : 0}))
            {
                return BadRequest("Error: both student and course IDs must be specified (with non-zero value)");
            }

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await StudentsCoursesRepository.DeleteStudentCourseAsync(connection,
                        (studentCourse["StudentID"], studentCourse["CourseID"]));

                    if (numberOfAffectedRows == 0)
                        return NotFound("Student with this student ID doesn't study on course with this course ID");
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE api/<StudentsCoursesController>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllStudentsCourses()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await StudentsCoursesRepository.DeleteAllStudentsCoursesAsync(connection);

                    if (numberOfAffectedRows == 0)
                        return NotFound("There aren't any students on any courses");
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

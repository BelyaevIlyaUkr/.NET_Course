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
    public class HometasksController : ControllerBase
    {
        IConfiguration Configuration { get; }

        public HometasksController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<HometasksController>
        [HttpGet]
        public async Task<ActionResult<List<HomeTask>>> GetAllHometasks()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    return await HometasksRepository.GetAllHomeTasksAsync(connection);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // GET api/<HometasksController>/5
        [HttpGet("{hometaskID}")]
        public async Task<ActionResult<HomeTask>> GetHometaskWithID(int hometaskID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { hometaskID }))
                return BadRequest("Error: hometask ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var homeTask = await HometasksRepository.GetDefiniteHometask(connection, hometaskID);

                    if (homeTask == null)
                        return NotFound("There isn't hometask with such ID in database");

                    return homeTask;
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // POST api/<HometasksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> hometask)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { hometask.ContainsKey("Name") ? hometask["Name"] : null,
                hometask.ContainsKey("Description") ? hometask["Description"] : null, hometask.ContainsKey("TaskDate") ? hometask["TaskDate"] : null,
                hometask.ContainsKey("SerialNumber") ? hometask["SerialNumber"] : null, hometask.ContainsKey("CourseID") 
                ? hometask["CourseID"] : null}))
            {
                return BadRequest("Error: all hometask input data must be specified");
            }

            hometask["Name"] = hometask["Name"].Trim();
            hometask["Description"] = hometask["Description"].Trim();
            hometask["TaskDate"] = hometask["TaskDate"].Trim();
            hometask["SerialNumber"] = hometask["SerialNumber"].Trim();
            hometask["CourseID"] = hometask["CourseID"].Trim();

            if (!Validation.ValidateDateTimeAndGetParsed(hometask["TaskDate"], out DateTime resultTaskDate))
                return BadRequest("Error: task date is incorrect");

            if (!Validation.ValidateIntAndGetParsed(hometask["SerialNumber"], out int resultSerialNumber) || resultSerialNumber < 0)
                return BadRequest("Error: serial number is incorrect");

            if (!Validation.ValidateIntAndGetParsed(hometask["CourseID"], out int resultCourseID))
                return BadRequest("Error: course ID is incorrect");

            var resultHometask = new HomeTask
            {
                Name = hometask["Name"],
                Description = hometask["Description"],
                TaskDate = resultTaskDate,
                SerialNumber = resultSerialNumber,
                CourseID = resultCourseID
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    await HometasksRepository.CreateHomeTaskAsync(connection, resultHometask);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(resultHometask);
        }

        // PUT api/<HometasksController>/5
        [HttpPut("{hometaskID}")]
        public async Task<IActionResult> Put(int hometaskID, [FromBody] Dictionary<string, string> hometask)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { hometaskID, hometask.ContainsKey("Name") ? hometask["Name"] : null,
                hometask.ContainsKey("Description") ? hometask["Description"] : null, hometask.ContainsKey("TaskDate") ? hometask["TaskDate"] : null,
                hometask.ContainsKey("SerialNumber") ? hometask["SerialNumber"] : null, hometask.ContainsKey("CourseID")
                ? hometask["CourseID"] : null}))
            {
                return BadRequest("Error: all hometask input data must be specified");
            }

            hometask["Name"] = hometask["Name"].Trim();
            hometask["Description"] = hometask["Description"].Trim();
            hometask["TaskDate"] = hometask["TaskDate"].Trim();
            hometask["SerialNumber"] = hometask["SerialNumber"].Trim();
            hometask["CourseID"] = hometask["CourseID"].Trim();

            if (!Validation.ValidateDateTimeAndGetParsed(hometask["TaskDate"], out DateTime resultTaskDate))
                return BadRequest("Error: task date is incorrect");

            if (!Validation.ValidateIntAndGetParsed(hometask["SerialNumber"], out int resultSerialNumber) || resultSerialNumber < 0)
                return BadRequest("Error: serial number is incorrect");

            if (!Validation.ValidateIntAndGetParsed(hometask["CourseID"], out int resultCourseID))
                return BadRequest("Error: course ID is incorrect");

            var resultHometask = new HomeTask
            {
                HomeTaskID = hometaskID,
                Name = hometask["Name"],
                Description = hometask["Description"],
                TaskDate = resultTaskDate,
                SerialNumber = resultSerialNumber,
                CourseID = resultCourseID
            };

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    await HometasksRepository.UpdateHomeTaskAsync(connection, resultHometask);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE api/<HometasksController>/5
        [HttpDelete("{hometaskID}")]
        public async Task<IActionResult> DeleteHometaskWithID(int hometaskID)
        {
            if (Validation.IsAnyInputObjectDataNotSpecified(new List<object> { hometaskID }))
                return BadRequest("Error: hometask ID must be specified (with non-zero value)");

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await HometasksRepository.DeleteGradeAsync(connection, hometaskID);

                    if (numberOfAffectedRows == 0)
                        return NotFound("Hometask with such id isn't found in database");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE api/<HometasksController>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllHometasks()
        {
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    connection.Open();

                    var numberOfAffectedRows = await HometasksRepository.DeleteAllHomeTasksAsync(connection);

                    if (numberOfAffectedRows == 0)
                        return NotFound("There aren't any hometasks in database");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudyManager.Models;
using StudyManager.DataAccess.ADO;
using System.Globalization;

namespace HomeTask8.Pages
{
    public class AddModel : PageModel
    {
        SqlConnection Connection { get; }

        [Display(Name = "Object type")]
        [BindProperty(SupportsGet = true)]
        public string SelectedInfoType { get; set; }

        public List<SelectListItem> InfoTypes { get; }

        [BindProperty(SupportsGet = true)]
        public string FirstInputField { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SecondInputField { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ThirdInputField { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FourthInputField { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FifthInputField { get; set; }

        public string ExceptionMessage { get; set; }

        public List<object> Objects { get; set; }

        public AddModel(IConfiguration configuration)
        {
            //Creating connection object one time, here, because it is performance expensive operation.
            Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            InfoTypes = new List<SelectListItem> {
                new SelectListItem { Value = "student", Text = "student" },
                new SelectListItem { Value = "course", Text = "course" },
                new SelectListItem { Value = "lecturer", Text = "lecturer" },
                new SelectListItem { Value = "hometask", Text = "hometask" },
                new SelectListItem { Value = "grade", Text = "grade" },
            };

            Objects = new List<object>();
        }

        public async Task OnPostAsync()
        {
            try
            {
                Connection.Open();

                ExceptionMessage = null;

                Objects.Clear();

                var dateFormat = "dd/MM/yyyy";

                switch (SelectedInfoType)
                {
                    case "student":
                        var newStudent = new Student
                        {
                            FirstName = FirstInputField,
                            LastName = SecondInputField,
                            PhoneNumber = ThirdInputField,
                            Email = FourthInputField,
                            Github = FifthInputField
                        };
                        await Repository.CreateStudentAsync(Connection, newStudent);
                        Objects.AddRange(await Repository.GetAllStudentsAsync(Connection));
                        break;
                    case "course":
                        var newCourse = new Course
                        {
                            Name = FirstInputField,
                            StartDate = DateTime.ParseExact(SecondInputField, dateFormat,CultureInfo.InvariantCulture),
                            EndDate = DateTime.ParseExact(ThirdInputField, dateFormat, CultureInfo.InvariantCulture),
                            PassingScore = int.Parse(FourthInputField)
                        };
                        await Repository.CreateCourseAsync(Connection, newCourse);
                        Objects.AddRange(await Repository.GetAllCoursesAsync(Connection));
                        break;
                    case "lecturer":
                        var newLecturer = new Lecturer
                        {
                            Name = FirstInputField,
                            BirthDate = DateTime.ParseExact(SecondInputField, dateFormat, CultureInfo.InvariantCulture)
                        };
                        await Repository.CreateLecturerAsync(Connection, newLecturer);
                        Objects.AddRange(await Repository.GetAllLecturersAsync(Connection));
                        break;
                    case "hometask":
                        var newHometask = new HomeTask
                        {
                            Name = FirstInputField,
                            Description = SecondInputField,
                            TaskDate = DateTime.ParseExact(ThirdInputField, dateFormat, CultureInfo.InvariantCulture),
                            SerialNumber = int.Parse(FourthInputField),
                            CourseID = int.Parse(FifthInputField)
                        };
                        await Repository.CreateHomeTaskAsync(Connection, newHometask);
                        Objects.AddRange(await Repository.GetAllHomeTasksAsync(Connection));
                        break;
                    case "grade":
                        var newGrade = new Grade
                        {
                            GradeDate = DateTime.ParseExact(FirstInputField, dateFormat, CultureInfo.InvariantCulture),
                            IsComplete = Convert.ToBoolean(SecondInputField),
                            HomeTaskID = int.Parse(ThirdInputField),
                            StudentID = int.Parse(FourthInputField)
                        };
                        await Repository.CreateGradeAsync(Connection, newGrade);
                        Objects.AddRange(await Repository.GetAllGradesAsync(Connection));
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage = "Error: " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}

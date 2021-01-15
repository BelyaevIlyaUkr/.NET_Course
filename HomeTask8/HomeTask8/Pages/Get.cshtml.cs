using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;

namespace HomeTask8.Pages
{
    public class GetModel : PageModel
    {
        SqlConnection Connection { get; }
        
        public List<object> Objects { get; set; }

        [Display(Name = "Information type")]
        [BindProperty (SupportsGet=true)]
        public string SelectedInfoType { get; set; }

        public List<SelectListItem> InfoTypes { get; }

        [BindProperty(SupportsGet = true)]
        public int FilterField { get; set; }

        public string ExceptionMessage { get; set; }

        public GetModel(IConfiguration configuration)
        {
            //Creating connection object one time, here, because it is performance expensive operation.
            Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            InfoTypes = new List<SelectListItem> {
                new SelectListItem { Value = "allStudents", Text = "All students" },
                new SelectListItem { Value = "allCourses", Text = "All courses" },
                new SelectListItem { Value = "allLecturers", Text = "All lecturers" },
                new SelectListItem { Value = "allHomeTasks", Text = "All hometasks" },
                new SelectListItem { Value = "allGrades", Text = "All grades" },
                new SelectListItem { Value = "allCoursesForStudent", Text = "All courses for student" },
                new SelectListItem { Value = "allStudentsInCourse", Text = "All students in course" },
                new SelectListItem { Value = "allLecturersForCourse", Text = "All lecturers for course" },
                new SelectListItem { Value = "allCoursesWithDefiniteLecturer", Text = "All courses with definite lecturer" }
            };

            Objects = new List<object>();
        }

        public async Task OnGetAsync()
        {
            try
            {
                Connection.Open();

                ExceptionMessage = null;

                Objects.Clear();

                switch (SelectedInfoType)
                {
                    case "allStudents":
                        Objects.AddRange(await Repository.GetAllStudentsAsync(Connection));
                        break;
                    case "allCourses":
                        Objects.AddRange(await Repository.GetAllCoursesAsync(Connection));
                        break;
                    case "allLecturers":
                        Objects.AddRange(await Repository.GetAllLecturersAsync(Connection));
                        break;
                    case "allHomeTasks":
                        Objects.AddRange(await Repository.GetAllHomeTasksAsync(Connection));
                        break;
                    case "allGrades":
                        Objects.AddRange(await Repository.GetAllGradesAsync(Connection));
                        break;
                    case "allCoursesForStudent":
                        if (!IsFilterFieldFilled())
                            throw new Exception("student ID field must be filled (with non-zero value)");
                        Objects.AddRange(await Repository.GetAllCoursesForStudentAsync(Connection, FilterField));
                        break;
                    case "allStudentsInCourse":
                        if (!IsFilterFieldFilled())
                            throw new Exception("course ID field must be filled (with non-zero value)");
                        Objects.AddRange(await Repository.GetAllStudentsInCourseAsync(Connection, FilterField));
                        break;
                    case "allLecturersForCourse":
                        if (!IsFilterFieldFilled())
                            throw new Exception("course ID field must be filled (with non-zero value)");
                        Objects.AddRange(await Repository.GetAllLecturersForCourseAsync(Connection, FilterField));
                        break;
                    case "allCoursesWithDefiniteLecturer":
                        if (!IsFilterFieldFilled())
                            throw new Exception("lecturer ID field must be filled (with non-zero value)");
                        Objects.AddRange(await Repository.GetAllCoursesWithDefiniteLecturerAsync(Connection, FilterField));
                        break;
                }
            }
            catch(Exception ex)
            {
                ExceptionMessage = "Error: " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
        }

        bool IsFilterFieldFilled()
        {
            if (FilterField == 0)
                return false;

            return true;
        }
    }
}

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
        
        public ArrayList Objects { get; set; }

        [Display(Name = "Information type")]
        public string SelectedInfoType { get; set; }

        public List<SelectListItem> InfoTypes { get; }

        public GetModel(IConfiguration configuration)
        {
            //Creating connection object one time, here, because it is performance expensive operation.
            Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            Objects = new ArrayList();

            InfoTypes = new List<SelectListItem> {
                new SelectListItem { Text = "Choose Type", Disabled = true, Selected = true },
                new SelectListItem { Value = "allStudents", Text = "All students" },
                new SelectListItem { Value = "allCourses", Text = "All courses" },
                new SelectListItem { Value = "allLecturers", Text = "All lecturers" },
                new SelectListItem { Value = "allHomeTasks", Text = "All hometasks" },
                new SelectListItem { Value = "allGrades", Text = "All grades" },
            };
        }

        public void OnGet()
        {
            try
            {
                Connection.Open();

                switch (SelectedInfoType)
                {
                    case "allStudents":
                        Repository.GetAllStudentsAsync(Connection);
                        break;
                    case "allCourses":
                        Repository.GetAllCoursesAsync(Connection);
                        break;
                    case "allLecturers":
                        Repository.GetAllLecturersAsync(Connection);
                        break;
                    case "allHomeTasks":
                        Repository.GetAllHomeTasksAsync(Connection);
                        break;
                    case "allGrades":
                        Repository.GetAllGradesAsync(Connection);
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Operation can't be completed: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}

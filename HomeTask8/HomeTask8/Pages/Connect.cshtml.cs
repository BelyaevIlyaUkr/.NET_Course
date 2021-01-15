using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudyManager.DataAccess.ADO;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace HomeTask8.Pages
{
    public class ConnectModel : PageModel
    {
        SqlConnection Connection { get; }

        public List<object> Objects { get; set; }

        [Display(Name = "Connection type")]
        [BindProperty]
        public string SelectedInfoType { get; set; }

        public List<SelectListItem> InfoTypes { get; }

        [BindProperty]
        public int FirstInputField { get; set; }

        [Display(Name = "Course ID")]
        [BindProperty]
        public int SecondInputField { get; set; }

        public string ExceptionMessage { get; set; }

        public ConnectModel(IConfiguration configuration)
        {
            //Creating connection object one time, here, because it is performance expensive operation.
            Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            InfoTypes = new List<SelectListItem> {
                new SelectListItem { Value = "studentToCourse", Text = "Student to course" },
                new SelectListItem { Value = "lecturerToCourse", Text = "Lecturer to course" }
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

                switch (SelectedInfoType)
                {
                    case null:
                        throw new Exception("type of information isn't chosed");
                    case "studentToCourse":
                        if (!AreAllInputFieldsFilled())
                            throw new Exception("All input fields must be filled (with non-zero value)");

                        await Repository.CreateStudentCourseAsync(Connection, (FirstInputField, SecondInputField));
                        Objects.AddRange(await Repository.GetAllStudentsInCourseAsync(Connection, SecondInputField));
                        break;
                    case "lecturerToCourse":
                        if (!AreAllInputFieldsFilled())
                            throw new Exception("All input fields must be filled (with non-zero value)");

                        await Repository.CreateCourseLecturerAsync(Connection, (SecondInputField, FirstInputField));
                        Objects.AddRange(await Repository.GetAllLecturersForCourseAsync(Connection, SecondInputField));
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

        bool AreAllInputFieldsFilled()
        {
            if (FirstInputField == 0)
                return false;

            if (SecondInputField == 0)
                return false;

            return true;
        }
    }
}

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
    public class DisconnectModel : PageModel
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

        public string ResultMessage { get; set; }

        public DisconnectModel(IConfiguration configuration)
        {
            //Creating connection object one time, here, because it is performance expensive operation.
            Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            InfoTypes = new List<SelectListItem> {
                new SelectListItem { Value = "studentFromCourse", Text = "Student from course" },
                new SelectListItem { Value = "lecturerFromCourse", Text = "Lecturer from course" },
                new SelectListItem { Value = "allStudentsFromAllCourses", Text = "All students from all courses" },
                new SelectListItem { Value = "allLecturersFromAllCourses", Text = "All lecturers from all courses" }
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

                ResultMessage = "All specified objects were disconnected successfully";

                switch (SelectedInfoType)
                {
                    case null:
                        throw new Exception("type of information isn't chosed");
                    case "studentFromCourse":
                        if (!AreAllInputFieldsFilled())
                            throw new Exception("All input fields must be filled (with non-zero value)");

                        var numberOfAffectedStudentsCoursesRows = await Repository.DeleteStudentCourseAsync(Connection, (FirstInputField, SecondInputField));
                        
                        if (numberOfAffectedStudentsCoursesRows == 0)
                            ResultMessage = "There isn't student with such ID that studies on this course";
                        else
                        {
                            ResultMessage = "Student was disconnected from course successfully. List of students on this course:";
                            Objects.AddRange(await Repository.GetAllStudentsInCourseAsync(Connection, SecondInputField));
                        }
                        break;
                    case "lecturerFromCourse":
                        if (!AreAllInputFieldsFilled())
                            throw new Exception("All input fields must be filled (with non-zero value)");

                        var numberOfAffectedCoursesLecturersRows = await Repository.DeleteCourseLecturerAsync(Connection, (SecondInputField, FirstInputField));

                        if (numberOfAffectedCoursesLecturersRows == 0)
                            ResultMessage = "There isn't lecturer with such ID that teaches on this course";
                        else
                        {
                            ResultMessage = "Lecturer was disconnected from course successfully. List of lecturers on this course:";
                            Objects.AddRange(await Repository.GetAllLecturersForCourseAsync(Connection, SecondInputField));
                        }
                        break;
                    case "allStudentsFromAllCourses":
                        var numberOfDeletedStudentsCoursesRows = await Repository.DeleteAllStudentCoursesAsync(Connection);

                        if (numberOfDeletedStudentsCoursesRows == 0)
                            ResultMessage = "There aren't any students on any courses";
                        break;
                    case "allLecturersFromAllCourses":
                        var numberOfDeletedCoursesLecturersRows = await Repository.DeleteAllCoursesLecturersAsync(Connection);

                        if (numberOfDeletedCoursesLecturersRows == 0)
                            ResultMessage = "There aren't any lecturers on any courses";
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

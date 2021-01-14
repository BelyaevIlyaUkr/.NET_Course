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
    public class DeleteModel : PageModel
    {
        SqlConnection Connection { get; }

        public List<object> Objects { get; set; }

        [Display(Name = "Information type")]
        [BindProperty(SupportsGet = true)]
        public string SelectedInfoType { get; set; }

        public List<SelectListItem> InfoTypes { get; }

        [BindProperty(SupportsGet = true)]
        public int FilterField { get; set; }

        public string ExceptionMessage { get; set; }

        public string ResultMessage { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            //Creating connection object one time, here, because it is performance expensive operation.
            Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            InfoTypes = new List<SelectListItem> {
                new SelectListItem { Value = "allStudents", Text = "All students" },
                new SelectListItem { Value = "allCourses", Text = "All courses" },
                new SelectListItem { Value = "allLecturers", Text = "All lecturers" },
                new SelectListItem { Value = "allHomeTasks", Text = "All hometasks" },
                new SelectListItem { Value = "allGrades", Text = "All grades" },
                new SelectListItem { Value = "studentWithDefiniteID", Text = "Student with definite ID" },
                new SelectListItem { Value = "courseWithDefiniteID", Text = "Course with definite ID" },
                new SelectListItem { Value = "lecturerWithDefiniteID", Text = "Lecturer with definite ID" },
                new SelectListItem { Value = "hometaskWithDefiniteID", Text = "Hometask with definite ID" },
                new SelectListItem { Value = "gradeWithDefiniteID", Text = "Grade with definite ID" },
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

                ResultMessage = "All specified objects were deleted successfully";

                switch (SelectedInfoType)
                {
                    case "allStudents":
                        var numberOfDeletedStudentsRows = await Repository.DeleteAllStudentsAsync(Connection);

                        if (numberOfDeletedStudentsRows == 0)
                            ResultMessage = "There aren't any students in database";
                        else
                            Objects.AddRange(await Repository.GetAllStudentsAsync(Connection));
                        break;
                    case "allCourses":
                        var numberOfDeletedCoursesRows = await Repository.DeleteAllCoursesAsync(Connection);

                        if (numberOfDeletedCoursesRows == 0)
                            ResultMessage = "There aren't any courses in database";
                        else
                            Objects.AddRange(await Repository.GetAllCoursesAsync(Connection));
                        break;
                    case "allLecturers":
                        var numberOfDeletedLecturersRows = await Repository.DeleteAllLecturersAsync(Connection);

                        if (numberOfDeletedLecturersRows == 0)
                            ResultMessage = "There aren't any lecturers in database";
                        else
                            Objects.AddRange(await Repository.GetAllLecturersAsync(Connection));
                        break;
                    case "allHomeTasks":
                        var numberOfDeletedHometasksRows = await Repository.DeleteAllHomeTasksAsync(Connection);

                        if (numberOfDeletedHometasksRows == 0)
                            ResultMessage = "There aren't any hometask in database";
                        else
                            Objects.AddRange(await Repository.GetAllHomeTasksAsync(Connection));
                        break;
                    case "allGrades":
                        var numberOfDeletedGradesRows = await Repository.DeleteAllGradesAsync(Connection);

                        if (numberOfDeletedGradesRows == 0)
                            ResultMessage = "There aren't any grades in database";
                        else
                            Objects.AddRange(await Repository.GetAllGradesAsync(Connection));
                        break;
                    case "studentWithDefiniteID":
                        var numberOfAffectedStudentRows = await Repository.DeleteStudentAsync(Connection, FilterField);

                        if (numberOfAffectedStudentRows == 0)
                            ResultMessage = "There isn't student with such ID in database";
                        else
                            ResultMessage = "Object was deleted successfully";
                        Objects.AddRange(await Repository.GetAllStudentsAsync(Connection));
                        break;
                    case "courseWithDefiniteID":
                        var numberOfAffectedCourseRows = await Repository.DeleteCourseAsync(Connection, FilterField);

                        if (numberOfAffectedCourseRows == 0)
                            ResultMessage = "There isn't course with such ID in database";
                        else
                            ResultMessage = "Object was deleted successfully";
                        Objects.AddRange(await Repository.GetAllCoursesAsync(Connection));
                        break;
                    case "lecturerWithDefiniteID":
                        var numberOfAffectedLecturerRows = await Repository.DeleteLecturerAsync(Connection, FilterField);

                        if (numberOfAffectedLecturerRows == 0)
                            ResultMessage = "There isn't lecturer with such ID in database";
                        else
                            ResultMessage = "Object was deleted successfully";
                        Objects.AddRange(await Repository.GetAllLecturersAsync(Connection));
                        break;
                    case "hometaskWithDefiniteID":
                        var numberOfAffectedHometaskRows = await Repository.DeleteHomeTaskAsync(Connection, FilterField);

                        if (numberOfAffectedHometaskRows == 0)
                            ResultMessage = "There isn't hometask with such ID in database";
                        else
                            ResultMessage = "Object was deleted successfully";
                        Objects.AddRange(await Repository.GetAllHomeTasksAsync(Connection));
                        break;
                    case "gradeWithDefiniteID":
                        var numberOfAffectedGradeRows = await Repository.DeleteGradeAsync(Connection, FilterField);

                        if (numberOfAffectedGradeRows == 0)
                            ResultMessage = "There isn't grade with such ID in database";
                        else
                            ResultMessage = "Object was deleted successfully";
                        Objects.AddRange(await Repository.GetAllGradesAsync(Connection));
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}

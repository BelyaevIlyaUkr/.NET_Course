using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class CoursesRepository
    {
        private static Course GetCourse(SqlDataReader reader)
        {
            var courseID = reader.GetInt32(0);
            var name = reader.GetString(1);
            var startDate = reader.GetDateTime(2);
            var endDate = reader.GetDateTime(3);
            var passingScore = reader.GetInt32(4);

            return new Course { CourseID = courseID, Name = name, StartDate = startDate, EndDate = endDate, PassingScore = passingScore };
        }


        public static async Task<List<Course>> GetAllCoursesAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT CourseID, Name, StartDate, EndDate, PassingScore FROM Courses", connection);

            var courses = new List<Course>();

            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var course = GetCourse(reader);
                        courses.Add(course);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }

            return courses;
        }

        public static async Task<Course> GetDefiniteCourse(SqlConnection connection, int courseID)
        {
            SqlCommand getCourseCommand = new SqlCommand($"SELECT CourseID, Name, StartDate, EndDate, " +
                    $"PassingScore FROM Courses WHERE CourseID = {courseID}", connection);

            try
            {
                using (var reader = await getCourseCommand.ExecuteReaderAsync())
                {
                    if (reader.Read())
                        return GetCourse(reader);
                }
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }

            return null;
        }

        public static async Task CreateCourseAsync(SqlConnection connection, Course course)
        {
            var createCommand = new SqlCommand("INSERT INTO Courses (Name,StartDate,EndDate,PassingScore)" +
            "VALUES (@name,@startDate,@endDate,@passingScore)", connection);

            createCommand.Parameters.AddWithValue("@name", course.Name);
            createCommand.Parameters.AddWithValue("@startDate", course.StartDate);
            createCommand.Parameters.AddWithValue("@endDate", course.EndDate);
            createCommand.Parameters.AddWithValue("@passingScore", course.PassingScore);

            try
            {
                await createCommand.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
        }

        public static async Task<int> UpdateCourseAsync(SqlConnection connection, Course course)
        {
            var updateCommand = new SqlCommand("UPDATE Courses SET Name = @name," +
            "StartDate = @startDate, EndDate = @endDate, PassingScore = passingScore " +
            "WHERE CourseID = @courseID", connection);

            updateCommand.Parameters.AddWithValue("@courseID", course.CourseID);
            updateCommand.Parameters.AddWithValue("@name", course.Name);
            updateCommand.Parameters.AddWithValue("@startDate", course.StartDate);
            updateCommand.Parameters.AddWithValue("@endDate", course.EndDate);
            updateCommand.Parameters.AddWithValue("@passingScore", course.PassingScore);

            try
            {
                var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
        }

        public static async Task<int> DeleteCourseAsync(SqlConnection connection, int courseID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses WHERE CourseID = @courseID", connection);

            deleteCommand.Parameters.AddWithValue("@courseID", courseID);

            try
            {
                var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
        }

        public static async Task<int> DeleteAllCoursesAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses", connection);

            try
            {
                var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}

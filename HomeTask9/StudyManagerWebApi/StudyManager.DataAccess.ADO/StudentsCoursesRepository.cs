using StudyManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudyManager.DataAccess.ADO
{
    public class StudentsCoursesRepository
    {
        public static async Task CreateStudentCourseAsync(SqlConnection connection, (int studentID, int courseID) studentCourse)
        {
            var createCommand = new SqlCommand("INSERT INTO Students_Courses (StudentID, CourseID)" +
                " VALUES (@studentID, @courseID)", connection);

            createCommand.Parameters.AddWithValue("@studentID", studentCourse.studentID);
            createCommand.Parameters.AddWithValue("@courseID", studentCourse.courseID);
            try
            {
                await createCommand.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                throw new Exception("There isn't student with this student ID or/and course with this course ID or this " +
                    "student is already connected to course");
            }
        }

        public static async Task<int> DeleteStudentCourseAsync(SqlConnection connection, (int studentID, int courseID) studentCourse)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students_Courses WHERE StudentID = @studentID " +
                "AND CourseID = @courseID", connection);

            deleteCommand.Parameters.AddWithValue("@studentID", studentCourse.studentID);
            deleteCommand.Parameters.AddWithValue("@courseID", studentCourse.courseID);

            try
            {
                var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
        }


        public static async Task<int> DeleteAllStudentsCoursesAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students_Courses", connection);

            try
            {
                var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
        }

        public static async Task<List<Course>> GetAllCoursesForStudentAsync(SqlConnection connection, int studentID)
        {
            SqlCommand getCoursesIDCommand = new SqlCommand($"SELECT CourseID FROM Students_Courses " +
                $"WHERE StudentID = {studentID}", connection);

            var coursesIDs = new List<int>();

            try
            {
                using (var reader = await getCoursesIDCommand.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var courseID = reader.GetInt32(0);

                        coursesIDs.Add(courseID);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }

            var courses = new List<Course>();

            foreach (var courseID in coursesIDs)
            {
                courses.Add(await CoursesRepository.GetDefiniteCourse(connection, courseID));
            }

            return courses;
        }

        public static async Task<List<Student>> GetAllStudentsInCourseAsync(SqlConnection connection, int courseID)
        {
            SqlCommand getStudentsIDCommand = new SqlCommand($"SELECT StudentID FROM Students_Courses " +
                $"WHERE CourseID = {courseID}", connection);

            var studentsIDs = new List<int>();

            try
            {
                using (var reader = await getStudentsIDCommand.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var studentID = reader.GetInt32(0);

                        studentsIDs.Add(studentID);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }

            var students = new List<Student>();

            foreach (var studentID in studentsIDs)
            {
                students.Add(await StudentsRepository.GetDefiniteStudent(connection, studentID));
            }

            return students;
        }

        public static async Task<List<Dictionary<string, int>>> GetAllStudentsCoursesAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT StudentID, CourseID FROM Students_Courses", connection);

            var studentsCourses = new List<Dictionary<string, int>>();

            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var studentCourse = new Dictionary<string, int>
                    {
                        {"StudentID", reader.GetInt32(0) },
                        {"CourseID", reader.GetInt32(1) }
                    };

                        studentsCourses.Add(studentCourse);
                    }
                }

                return studentsCourses;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class StudentsRepository
    {
        private static Student GetStudent(SqlDataReader reader)
        {
            var studentID = reader.GetInt32(0);
            var firstName = reader.GetString(1);
            var lastName = reader.GetString(2);
            var phoneNumber = reader.GetString(3);
            var email = reader.GetString(4);
            var github = reader.GetString(5);

            return new Student { StudentID = studentID, FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber, Email = email, Github = github };
        }


        public static async Task<List<Student>> GetAllStudentsAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT StudentID, FirstName, LastName, PhoneNumber, Email, Github FROM Students", connection);

            var students = new List<Student>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var student = GetStudent(reader);
                    students.Add(student);
                }
            }

            return students;
        }

        public static async Task<Student> GetDefiniteStudent(SqlConnection connection, int studentID)
        {
            SqlCommand getStudentCommand = new SqlCommand("SELECT StudentID, FirstName, LastName, " +
                $"PhoneNumber, Email, Github FROM Students WHERE StudentID = {studentID}", connection);

            using (var reader = await getStudentCommand.ExecuteReaderAsync())
            {
                if (reader.Read())
                    return GetStudent(reader);
            }

            return null;
        }

        public static async Task CreateStudentAsync(SqlConnection connection, Student student)
        {
            var createCommand = new SqlCommand("INSERT INTO Students (FirstName,LastName,PhoneNumber,Email,Github)" +
            "VALUES (@firstName,@lastName,@phoneNumber,@email,@github)", connection);

            createCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            createCommand.Parameters.AddWithValue("@lastName", student.LastName);
            createCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            createCommand.Parameters.AddWithValue("@email", student.Email);
            createCommand.Parameters.AddWithValue("@github", student.Github);

            await createCommand.ExecuteNonQueryAsync();
        }


        public static async Task<int> DeleteStudentAsync(SqlConnection connection, int studentID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students WHERE StudentID = @studentID", connection);

            deleteCommand.Parameters.AddWithValue("@studentID", studentID);
            
            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteAllStudentsAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> UpdateStudentAsync(SqlConnection connection, Student student)
        {
            var updateCommand = new SqlCommand("UPDATE Students SET FirstName = @firstName," +
            "LastName = @lastName, PhoneNumber = @phoneNumber, Email = @email, Github = @github " +
            "WHERE StudentID = @studentID", connection);

            updateCommand.Parameters.AddWithValue("@studentID", student.StudentID);
            updateCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            updateCommand.Parameters.AddWithValue("@lastName", student.LastName);
            updateCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            updateCommand.Parameters.AddWithValue("@email", student.Email);
            updateCommand.Parameters.AddWithValue("@github", student.Github);

            var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<List<(int courseID, int lecturerID)>> GetAllCoursesLecturersAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT CourseID, LecturerID FROM Courses_Lecturers", connection);

            var coursesLecturers = new List<(int courseID, int lecturerID)>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var courseLecturer = (reader.GetInt32(0), reader.GetInt32(1));
                    coursesLecturers.Add(courseLecturer);
                }
            }

            return coursesLecturers;
        }

        public static async Task CreateCourseLecturerAsync(SqlConnection connection, (int courseID, int lecturerID) courseLecturer)
        {
            var createCommand = new SqlCommand("INSERT INTO Courses_Lecturers (CourseID, LecturerID)" +
                " VALUES (@courseID, @lecturerID)", connection);

            createCommand.Parameters.AddWithValue("@courseID", courseLecturer.courseID);
            createCommand.Parameters.AddWithValue("@lecturerID", courseLecturer.lecturerID);

            try
            {
                await createCommand.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                throw new Exception("there isn't lecturer or/and course with such ID or " +
                    "this lecturer have already been connected to this course");
            }
        }

        public static async Task<int> DeleteCourseLecturerAsync(SqlConnection connection, (int courseID, int lecturerID) courseLecturer)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses_Lecturers WHERE CourseID = @courseID " +
                "AND LecturerID = @lecturerID", connection);

            deleteCommand.Parameters.AddWithValue("@courseID", courseLecturer.courseID);
            deleteCommand.Parameters.AddWithValue("@lecturerID", courseLecturer.lecturerID);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteAllCoursesLecturersAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses_Lecturers", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<List<Lecturer>> GetAllLecturersForCourseAsync(SqlConnection connection, int courseID)
        {
            SqlCommand getLecturersIDCommand = new SqlCommand($"SELECT LecturerID FROM Courses_Lecturers " +
                $"WHERE CourseID = {courseID}", connection);

            var lecturersIDs = new List<int>();

            using (var reader = await getLecturersIDCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var lecturerID = reader.GetInt32(0);

                    lecturersIDs.Add(lecturerID);
                }
            }

            var lecturers = new List<Lecturer>();

            foreach (var lecturerID in lecturersIDs)
            {
                SqlCommand getLecturersCommand = new SqlCommand($"SELECT LecturerID, Name, BirthDate " +
                        $"FROM Lecturers WHERE LecturerID = {lecturerID}", connection);

                using (var reader = await getLecturersCommand.ExecuteReaderAsync())
                {
                    reader.Read();

                    var lecturer = GetLecturer(reader);

                    lecturers.Add(lecturer);
                }
            }

            return lecturers;
        }

        public static async Task<List<Course>> GetAllCoursesWithDefiniteLecturerAsync(SqlConnection connection, int lecturerID)
        {
            SqlCommand getCoursesIDCommand = new SqlCommand($"SELECT CourseID FROM Courses_Lecturers " +
                $"WHERE LecturerID = {lecturerID}", connection);

            var coursesIDs = new List<int>();

            using (var reader = await getCoursesIDCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var courseID = reader.GetInt32(0);

                    coursesIDs.Add(courseID);
                }
            }

            var courses = new List<Course>();

            foreach (var courseID in coursesIDs)
            {
                SqlCommand getCoursesCommand = new SqlCommand($"SELECT CourseID, Name, StartDate, EndDate, " +
                        $"PassingScore FROM Courses WHERE CourseID = {courseID}", connection);

                using (var reader = await getCoursesCommand.ExecuteReaderAsync())
                {
                    reader.Read();

                    var course = GetCourse(reader);

                    courses.Add(course);
                }
            }

            return courses;
        }
    }
}

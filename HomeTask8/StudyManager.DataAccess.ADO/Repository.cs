using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class Repository
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

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var course = GetCourse(reader);
                    courses.Add(course);
                }
            }

            return courses;
        }

        public static async Task CreateCourseAsync(SqlConnection connection, Course course)
        {
            var createCommand = new SqlCommand("INSERT INTO Courses (Name,StartDate,EndDate,PassingScore)" +
            "VALUES (@name,@startDate,@endDate,@passingScore)", connection);

            createCommand.Parameters.AddWithValue("@name", course.Name);
            createCommand.Parameters.AddWithValue("@startDate", course.StartDate);
            createCommand.Parameters.AddWithValue("@endDate", course.EndDate);
            createCommand.Parameters.AddWithValue("@passingScore", course.PassingScore);

            await createCommand.ExecuteNonQueryAsync();
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

            var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteCourseAsync(SqlConnection connection, int courseID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses WHERE CourseID = @courseID", connection);

            deleteCommand.Parameters.AddWithValue("@courseID", courseID);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteAllCoursesAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        private static Lecturer GetLecturer(SqlDataReader reader)
        {
            var lecturerID = reader.GetInt32(0);
            var name = reader.GetString(1);
            var birthDate = reader.GetDateTime(2);

            return new Lecturer { LecturerID = lecturerID, Name = name, BirthDate = birthDate };
        }

        public static async Task<List<Lecturer>> GetAllLecturersAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT LecturerID, Name, BirthDate FROM Lecturers", connection);

            var lecturers = new List<Lecturer>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var lecturer = GetLecturer(reader);
                    lecturers.Add(lecturer);
                }
            }

            return lecturers;
        }

        public static async Task CreateLecturerAsync(SqlConnection connection, Lecturer lecturer)
        {
            var createCommand = new SqlCommand("INSERT INTO Lecturers (Name, BirthDate)" +
            "VALUES (@name, @birthDate)", connection);

            createCommand.Parameters.AddWithValue("@name", lecturer.Name);
            createCommand.Parameters.AddWithValue("@birthDate", lecturer.BirthDate);

            await createCommand.ExecuteNonQueryAsync();
        }

        public static async Task<int> UpdateLecturerAsync(SqlConnection connection, Lecturer lecturer)
        {
            var updateCommand = new SqlCommand("UPDATE Lecturers SET Name = @name," +
            "BirthDate = @birthDate WHERE LecturerID = @lecturerID", connection);

            updateCommand.Parameters.AddWithValue("@lecturerID", lecturer.LecturerID);
            updateCommand.Parameters.AddWithValue("@name", lecturer.Name);
            updateCommand.Parameters.AddWithValue("@birthDate", lecturer.BirthDate);

            var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;  
        }

        public static async Task<int> DeleteLecturerAsync(SqlConnection connection, int lecturerID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Lecturers WHERE LecturerID = @lecturerID", connection);

            deleteCommand.Parameters.AddWithValue("@lecturerID", lecturerID);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteAllLecturersAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Lecturers", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        private static HomeTask GetHomeTask(SqlDataReader reader)
        {
            var homeTaskID = reader.GetInt32(0);
            var name = reader.GetString(1);
            var description = reader.GetString(2);
            var taskDate = reader.GetDateTime(3);
            var serialNumber = reader.GetInt32(4);
            var courseID = reader.GetInt32(5);

            return new HomeTask
            {
                HomeTaskID = homeTaskID,
                Name = name,
                Description = description,
                TaskDate = taskDate,
                SerialNumber = serialNumber,
                CourseID = courseID
            };
        }

        public static async Task<List<HomeTask>> GetAllHomeTasksAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT HomeTaskID, Name, Description, TaskDate, " +
                "SerialNumber, CourseID FROM HomeTasks", connection);

            var homeTasks = new List<HomeTask>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var homeTask = GetHomeTask(reader);
                    homeTasks.Add(homeTask);
                }
            }

            return homeTasks;
        }

        public static async Task CreateHomeTaskAsync(SqlConnection connection, HomeTask hometask)
        {
            var createCommand = new SqlCommand("INSERT INTO HomeTasks (Name, Description, TaskDate," +
                "SerialNumber, CourseID) VALUES (@name, @description, @taskDate, @serialNumber, @courseID)", connection);

            createCommand.Parameters.AddWithValue("@name", hometask.Name);
            createCommand.Parameters.AddWithValue("@description", hometask.Description);
            createCommand.Parameters.AddWithValue("@taskDate", hometask.TaskDate);
            createCommand.Parameters.AddWithValue("@serialNumber", hometask.SerialNumber);
            createCommand.Parameters.AddWithValue("@courseID", hometask.CourseID);

            try
            {
                await createCommand.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                throw new Exception("there isn't course with such ID in database");
            }
            catch (Exception)
            {
                throw new Exception("something went wrong");
            }
        }

        public static async Task<int> UpdateHomeTaskAsync(SqlConnection connection, HomeTask hometask)
        {
            var updateCommand = new SqlCommand("UPDATE Hometasks SET Name = @name," +
            "Description = @description, TaskDate = @taskDate, SerialNumber = @serialNumber," +
            "CourseID = courseID WHERE HomeTaskID = @hometaskID", connection);

            updateCommand.Parameters.AddWithValue("@hometaskID", hometask.HomeTaskID);
            updateCommand.Parameters.AddWithValue("@name", hometask.Name);
            updateCommand.Parameters.AddWithValue("@description", hometask.Description);
            updateCommand.Parameters.AddWithValue("@taskDate", hometask.TaskDate);
            updateCommand.Parameters.AddWithValue("@serialNumber", hometask.SerialNumber);
            updateCommand.Parameters.AddWithValue("@courseID", hometask.CourseID);

            try
            {
                var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (SqlException)
            {
                throw new Exception("there isn't course with such ID in database");
            }
            catch (Exception)
            {
                throw new Exception("something went wrong");
            }
        }

        public static async Task<int> DeleteHomeTaskAsync(SqlConnection connection, int hometaskID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM HomeTasks WHERE HomeTaskID = @hometaskID", connection);

            deleteCommand.Parameters.AddWithValue("@hometaskID", hometaskID);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteAllHomeTasksAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM HomeTasks", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        private static Grade GetGrade(SqlDataReader reader)
        {
            var gradeID = reader.GetInt32(0);
            var gradeDate = reader.GetDateTime(1);
            var isComplete = reader.GetBoolean(2);
            var hometaskID = reader.GetInt32(3);
            var studentID = reader.GetInt32(4);

            return new Grade
            {
                GradeID = gradeID,
                GradeDate = gradeDate,
                IsComplete = isComplete,
                HomeTaskID = hometaskID,
                StudentID = studentID
            };
        }

        public static async Task<List<Grade>> GetAllGradesAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT GradeID, GradeDate, IsComplete, HomeTaskID," +
                "StudentID FROM Grades", connection);

            var grades = new List<Grade>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var grade = GetGrade(reader);
                    grades.Add(grade);
                }
            }

            return grades;
        }

        public static async Task CreateGradeAsync(SqlConnection connection, Grade grade)
        {
            var createCommand = new SqlCommand("INSERT INTO Grades (GradeDate, IsComplete, HomeTaskID," +
                "StudentID) VALUES (@gradeDate, @isComplete, @hometaskID, @studentID)", connection);

            createCommand.Parameters.AddWithValue("@gradeDate", grade.GradeDate);
            createCommand.Parameters.AddWithValue("@isComplete", grade.IsComplete);
            createCommand.Parameters.AddWithValue("@hometaskID", grade.HomeTaskID);
            createCommand.Parameters.AddWithValue("@studentID", grade.StudentID);

            try
            {
                await createCommand.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                throw new Exception("there isn't hometask with such ID or/and student with such ID in database");
            }
            catch (Exception)
            {
                throw new Exception("something went wrong");
            }
        }

        public static async Task<int> UpdateGradeAsync(SqlConnection connection, Grade grade)
        {
            var updateCommand = new SqlCommand("UPDATE Grades SET GradeDate = @gradeDate," +
            "IsComplete = @isComplete, HomeTaskID = @hometaskID, StudentID = @studentID " +
            "WHERE GradeID = @gradeID", connection);

            updateCommand.Parameters.AddWithValue("@gradeDate", grade.GradeDate);
            updateCommand.Parameters.AddWithValue("@isComplete", grade.IsComplete);
            updateCommand.Parameters.AddWithValue("@hometaskID", grade.HomeTaskID);
            updateCommand.Parameters.AddWithValue("@studentID", grade.StudentID);
            updateCommand.Parameters.AddWithValue("@gradeID", grade.GradeID);

            try
            {
                var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (SqlException)
            {
                throw new Exception("there isn't hometask or/and student with such ID in database");
            }
            catch (Exception)
            {
                throw new Exception("something went wrong");
            }
        }

        public static async Task<int> DeleteGradeAsync(SqlConnection connection, int gradeID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Grades WHERE GradeID = @gradeID", connection);

            deleteCommand.Parameters.AddWithValue("@gradeID", gradeID);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }


        public static async Task<int> DeleteAllGradesAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Grades", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<List<(int studentID, int courseID)>> GetAllStudentsCoursesAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT StudentID, CourseID FROM Students_Courses", connection);

            var studentsCourses = new List<(int studentID, int courseID)>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var studentCourse = (reader.GetInt32(0), reader.GetInt32(1));
                    studentsCourses.Add(studentCourse);
                }
            }

            return studentsCourses;
        }

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
                throw new Exception("there isn't student or/and course with such ID or " +
                    "this student have already been connected to this course");
            }
            catch (Exception)
            {
                throw new Exception("something went wrong");
            }
        }

        public static async Task<int> DeleteStudentCourseAsync(SqlConnection connection, (int studentID, int courseID) studentCourse)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students_Courses WHERE StudentID = @studentID " +
                "AND CourseID = @courseID", connection);

            deleteCommand.Parameters.AddWithValue("@studentID", studentCourse.studentID);
            deleteCommand.Parameters.AddWithValue("@courseID", studentCourse.courseID);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }


        public static async Task<int> DeleteAllStudentCoursesAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students_Courses", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<List<Course>> GetAllCoursesForStudentAsync(SqlConnection connection, int studentID)
        {
            SqlCommand getCoursesIDCommand = new SqlCommand($"SELECT CourseID FROM Students_Courses " +
                $"WHERE StudentID = {studentID}", connection);

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

        public static async Task<List<Student>> GetAllStudentsInCourseAsync(SqlConnection connection, int courseID)
        {
            SqlCommand getStudentsIDCommand = new SqlCommand($"SELECT StudentID FROM Students_Courses " +
                $"WHERE CourseID = {courseID}", connection);

            var studentsIDs = new List<int>();

            using (var reader = await getStudentsIDCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var studentID = reader.GetInt32(0);

                    studentsIDs.Add(studentID);
                }
            }

            var students = new List<Student>();

            foreach (var studentID in studentsIDs)
            {
                SqlCommand getStudentsCommand = new SqlCommand($"SELECT StudentID, FirstName, LastName, PhoneNumber, " +
                    $"Email, Github FROM Students WHERE StudentID = {studentID}", connection);

                using (var reader = getStudentsCommand.ExecuteReader())
                {
                    reader.Read();

                    var student = GetStudent(reader);

                    students.Add(student);
                }
            }

            return students;
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
            catch (Exception)
            {
                throw new Exception("something went wrong");
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

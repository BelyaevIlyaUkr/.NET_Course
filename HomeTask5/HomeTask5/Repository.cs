using System.Collections.Generic;
using System.Data.SqlClient;

namespace HomeTask5
{
    class Repository
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


        public static List<Student> GetAllStudents(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT StudentID, FirstName, LastName, PhoneNumber, Email, Github FROM Students", connection);

            var students = new List<Student>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var student = GetStudent(reader);
                    students.Add(student);
                }
            }

            return students;
        }


        public static Student CreateStudent(SqlConnection connection, Student student)
        {
            var createCommand = new SqlCommand("INSERT INTO Students (FirstName,LastName,PhoneNumber,Email,Github)" +
            "VALUES (@firstName,@lastName,@phoneNumber,@email,@github)", connection);

            createCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            createCommand.Parameters.AddWithValue("@lastName", student.LastName);
            createCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            createCommand.Parameters.AddWithValue("@email", student.Email);
            createCommand.Parameters.AddWithValue("@github", student.Github);

            createCommand.ExecuteNonQuery();

            return student;
        }


        public static void DeleteStudent(SqlConnection connection, int studentID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students WHERE StudentID = @studentID", connection);

            deleteCommand.Parameters.AddWithValue("@studentID", studentID);
            deleteCommand.ExecuteNonQuery();
        }

        public static void DeleteAllStudents(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students", connection);

            deleteCommand.ExecuteNonQuery();
        }

        public static void UpdateStudent(SqlConnection connection, Student student)
        {
            var updateCommand = new SqlCommand("UPDATE Students SET FirstName = @firstName," +
            "LastName = @lastName, PhoneNumber = @phoneNumber, Email = @email, Github = @github" +
            "WHERE StudentID = @studentID", connection);

            updateCommand.Parameters.AddWithValue("@studentID", student.StudentID);
            updateCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            updateCommand.Parameters.AddWithValue("@lastName", student.LastName);
            updateCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            updateCommand.Parameters.AddWithValue("@email", student.Email);
            updateCommand.Parameters.AddWithValue("@github", student.Github);

            updateCommand.ExecuteNonQuery();
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


        public static List<Course> GetAllCourses(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT CourseID, Name, StartDate, EndDate, PassingScore FROM Courses", connection);

            var courses = new List<Course>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var course = GetCourse(reader);
                    courses.Add(course);
                }
            }

            return courses;
        }

        public static Course CreateCourse(SqlConnection connection, Course course)
        {
            var createCommand = new SqlCommand("INSERT INTO Courses (Name,StartDate,EndDate,PassingScore)" +
            "VALUES (@name,@startDate,@endDate,@passingScore)", connection);

            createCommand.Parameters.AddWithValue("@name", course.Name);
            createCommand.Parameters.AddWithValue("@startDate", course.StartDate);
            createCommand.Parameters.AddWithValue("@endDate", course.EndDate);
            createCommand.Parameters.AddWithValue("@passingScore", course.PassingScore);

            createCommand.ExecuteNonQuery();

            return course;
        }

        public static void UpdateCourse(SqlConnection connection, Course course)
        {
            var updateCommand = new SqlCommand("UPDATE Courses SET Name = @name," +
            "StartDate = @startDate, EndDate = @endDate, PassingScore = passingScore," +
            "WHERE CourseID = @courseID", connection);

            updateCommand.Parameters.AddWithValue("@courseID", course.CourseID);
            updateCommand.Parameters.AddWithValue("@name", course.Name);
            updateCommand.Parameters.AddWithValue("@startDate", course.StartDate);
            updateCommand.Parameters.AddWithValue("@endDate", course.EndDate);
            updateCommand.Parameters.AddWithValue("@passingScore", course.PassingScore);

            updateCommand.ExecuteNonQuery();
        }

        public static void DeleteCourse(SqlConnection connection, int courseID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses WHERE CoursesID = @courseID", connection);

            deleteCommand.Parameters.AddWithValue("@courseID", courseID);

            deleteCommand.ExecuteNonQuery();
        }

        public static void DeleteAllCourses(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses", connection);

            deleteCommand.ExecuteNonQuery();
        }

        private static Lecturer GetLecturer(SqlDataReader reader)
        {
            var lecturerID = reader.GetInt32(0);
            var name = reader.GetString(1);
            var birthDate = reader.GetDateTime(2);

            return new Lecturer { LecturerID = lecturerID, Name = name, BirthDate = birthDate };
        }

        public static List<Lecturer> GetAllLecturers(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT LecturerID, Name, BirthDate FROM Students", connection);

            var lecturers = new List<Lecturer>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var lecturer = GetLecturer(reader);
                    lecturers.Add(lecturer);
                }
            }

            return lecturers;
        }

        public static Lecturer CreateLecturer(SqlConnection connection, Lecturer lecturer)
        {
            var createCommand = new SqlCommand("INSERT INTO Lecturers (Name, BirthDate)" +
            "VALUES (@name, @birthDate)", connection);

            createCommand.Parameters.AddWithValue("@name", lecturer.Name);
            createCommand.Parameters.AddWithValue("@birthDate", lecturer.BirthDate);

            createCommand.ExecuteNonQuery();

            return lecturer;
        }

        public static void UpdateLecturer(SqlConnection connection, Lecturer lecturer)
        {
            var updateCommand = new SqlCommand("UPDATE Lecturers SET Name = @name," +
            "BirthDate = @birthDate WHERE LecturerID = @lecturerID", connection);

            updateCommand.Parameters.AddWithValue("@lecturerID", lecturer.LecturerID);
            updateCommand.Parameters.AddWithValue("@name", lecturer.Name);
            updateCommand.Parameters.AddWithValue("@birthDate", lecturer.BirthDate);

            updateCommand.ExecuteNonQuery();
        }

        public static void DeleteLecturer(SqlConnection connection, int lecturerID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Lecturers WHERE LecturerID = @lecturerID", connection);

            deleteCommand.Parameters.AddWithValue("@lecturerID", lecturerID);

            deleteCommand.ExecuteNonQuery();
        }

        public static void DeleteAllLecturers(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Lecturers", connection);

            deleteCommand.ExecuteNonQuery();
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

        public static List<HomeTask> GetAllHomeTasks(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT HomeTaskID, Name, Description, TaskDate" +
                "SerialNumber, CourseID FROM HomeTasks", connection);

            var homeTasks = new List<HomeTask>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var homeTask = GetHomeTask(reader);
                    homeTasks.Add(homeTask);
                }
            }

            return homeTasks;
        }

        public static HomeTask CreateHomeTask(SqlConnection connection, HomeTask hometask)
        {
            var createCommand = new SqlCommand("INSERT INTO HomeTasks (Name, Description, TaskDate," +
                "SerialNumber, CourseID) VALUES (@name, @description, @taskDate, @serialNumber, @courseID)", connection);

            createCommand.Parameters.AddWithValue("@name", hometask.Name);
            createCommand.Parameters.AddWithValue("@description", hometask.Description);
            createCommand.Parameters.AddWithValue("@taskDate", hometask.TaskDate);
            createCommand.Parameters.AddWithValue("@serialNumber", hometask.SerialNumber);
            createCommand.Parameters.AddWithValue("@courseID", hometask.CourseID);

            createCommand.ExecuteNonQuery();

            return hometask;
        }

        public static void UpdateHomeTask(SqlConnection connection, HomeTask hometask)
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

            updateCommand.ExecuteNonQuery();
        }

        public static void DeleteHomeTask(SqlConnection connection, int hometaskID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM HomeTasks WHERE HomeTaskID = @hometaskID", connection);

            deleteCommand.Parameters.AddWithValue("@hometaskID", hometaskID);

            deleteCommand.ExecuteNonQuery();
        }

        public static void DeleteAllHomeTasks(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM HomeTasks", connection);

            deleteCommand.ExecuteNonQuery();
        }

        private static Grade GetGrade(SqlDataReader reader)
        {
            var gradeID = reader.GetInt32(0);
            var gradeDate = reader.GetDateTime(1);
            var isComplete = reader.GetInt32(2);
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

        public static List<Grade> GetAllGrades(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT GradeID, GradeDate, IsComplete, HomeTaskID," +
                "StudentID FROM HomeTasks", connection);

            var grades = new List<Grade>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var grade = GetGrade(reader);
                    grades.Add(grade);
                }
            }

            return grades;
        }

        public static Grade CreateGrade(SqlConnection connection, Grade grade)
        {
            var createCommand = new SqlCommand("INSERT INTO Grades (GradeDate, IsComplete, HomeTaskID," +
                "StudentID) VALUES (@gradeDate, @isComplete, @hometaskID, @studentID)", connection);

            createCommand.Parameters.AddWithValue("@gradeDate", grade.GradeDate);
            createCommand.Parameters.AddWithValue("@isComplete", grade.IsComplete);
            createCommand.Parameters.AddWithValue("@hometaskID", grade.HomeTaskID);
            createCommand.Parameters.AddWithValue("@studentID", grade.StudentID);

            createCommand.ExecuteNonQuery();

            return grade;
        }

        public static void UpdateGrade(SqlConnection connection, Grade grade)
        {
            var updateCommand = new SqlCommand("UPDATE Grades SET GradeDate = @gradeDate," +
            "IsComplete = @isComplete, HomeTaskID = @hometaskID, StudentID = @studentID," +
            "WHERE GradeID = @gradeID", connection);

            updateCommand.Parameters.AddWithValue("@gradeDate", grade.GradeDate);
            updateCommand.Parameters.AddWithValue("@isComplete", grade.IsComplete);
            updateCommand.Parameters.AddWithValue("@hometaskID", grade.HomeTaskID);
            updateCommand.Parameters.AddWithValue("@studentID", grade.StudentID);
            updateCommand.Parameters.AddWithValue("@gradeID", grade.GradeID);

            updateCommand.ExecuteNonQuery();
        }

        public static void DeleteGrade(SqlConnection connection, int gradeID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Grades WHERE GradeID = @gradeID", connection);

            deleteCommand.Parameters.AddWithValue("@gradeID", gradeID);

            deleteCommand.ExecuteNonQuery();
        }


        public static void DeleteAllGrades(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Grades", connection);

            deleteCommand.ExecuteNonQuery();
        }

        public static List<(int studentID, int courseID)> GetAllStudentsCourses(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT StudentID, CourseID FROM Students_Courses", connection);

            var studentsCourses = new List<(int studentID, int courseID)>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var studentCourse = (reader.GetInt32(0), reader.GetInt32(1));
                    studentsCourses.Add(studentCourse);
                }
            }

            return studentsCourses;
        }

        public static (int studentID, int courseID) CreateStudentCourse(SqlConnection connection, (int studentID, int courseID) studentCourse)
        {
            var createCommand = new SqlCommand("INSERT INTO Students_Courses (StudentID, CourseID)" +
                " VALUES (@studentID, @courseID)", connection);

            createCommand.Parameters.AddWithValue("@studentID", studentCourse.studentID);
            createCommand.Parameters.AddWithValue("@courseID", studentCourse.courseID);

            createCommand.ExecuteNonQuery();

            return studentCourse;
        }

        public static void DeleteStudentCourse(SqlConnection connection, (int studentID, int courseID) studentCourse)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students_Courses WHERE StudentID = @studentID " +
                "AND CourseID = @courseID", connection);

            deleteCommand.Parameters.AddWithValue("@studentID", studentCourse.studentID);
            deleteCommand.Parameters.AddWithValue("@courseID", studentCourse.courseID);

            deleteCommand.ExecuteNonQuery();
        }


        public static void DeleteAllStudentCourses(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Student_Courses", connection);

            deleteCommand.ExecuteNonQuery();
        }

        public static List<Course> GetAllCoursesForStudent(SqlConnection connection, int studentID)
        {
            SqlCommand getCoursesIDCommand = new SqlCommand($"SELECT CourseID FROM Students_Courses " +
                $"WHERE StudentID = {studentID}", connection);

            var courses = new List<Course>();

            using (var reader = getCoursesIDCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var courseID = reader.GetInt32(0);

                    SqlCommand getCoursesCommand = new SqlCommand($"SELECT CourseID, Name, StartDate, EndDate, " +
                        $"PassingScore FROM Courses WHERE CourseID = {courseID}", connection);

                    using (var subreader = getCoursesCommand.ExecuteReader())
                    {
                        subreader.Read();

                        var course = GetCourse(subreader);

                        courses.Add(course);
                    }
                }
            }

            return courses;
        }

        public static List<Student> GetAllStudentsForCourse(SqlConnection connection, int courseID)
        {
            SqlCommand getStudentsIDCommand = new SqlCommand($"SELECT StudentID FROM Students_Courses " +
                $"WHERE CourseID = {courseID}", connection);

            var students = new List<Student>();

            using (var reader = getStudentsIDCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var studentID = reader.GetInt32(0);

                    SqlCommand getStudentsCommand = new SqlCommand($"SELECT StudentID, FirstName, LastName, PhoneNumber, " +
                        $"Email, Github FROM Students WHERE StudentID = {studentID}", connection);

                    using (var subreader = getStudentsCommand.ExecuteReader())
                    {
                        subreader.Read();

                        var student = GetStudent(subreader);

                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public static List<(int courseID, int lecturerID)> GetAllCoursesLecturers(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT CourseID, LecturerID FROM Courses_Lecturers", connection);

            var coursesLecturers = new List<(int courseID, int lecturerID)>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var courseLecturer = (reader.GetInt32(0), reader.GetInt32(1));
                    coursesLecturers.Add(courseLecturer);
                }
            }

            return coursesLecturers;
        }

        public static (int studentID, int courseID) CreateCourseLecturer(SqlConnection connection, (int courseID, int lecturerID) courseLecturer)
        {
            var createCommand = new SqlCommand("INSERT INTO Courses_Lecturers (CourseID, LecturerID)" +
                " VALUES (@courseID, @lecturerID)", connection);

            createCommand.Parameters.AddWithValue("@courseID", courseLecturer.courseID);
            createCommand.Parameters.AddWithValue("@lecturerID", courseLecturer.lecturerID);

            createCommand.ExecuteNonQuery();

            return courseLecturer;
        }

        public static void DeleteCourseLecturer(SqlConnection connection, (int courseID, int lecturerID) courseLecturer)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses_Lecturers WHERE CourseID = @courseID " +
                "AND LecturerID = @lecturerID", connection);

            deleteCommand.Parameters.AddWithValue("@courseID", courseLecturer.courseID);
            deleteCommand.Parameters.AddWithValue("@lecturerID", courseLecturer.lecturerID);

            deleteCommand.ExecuteNonQuery();
        }

        public static void DeleteAllCoursesLecturers(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Courses_Lecturers", connection);

            deleteCommand.ExecuteNonQuery();
        }

        public static List<Lecturer> GetAllLecturersForCourse(SqlConnection connection, int courseID)
        {
            SqlCommand getLecturersIDCommand = new SqlCommand($"SELECT LecturerID FROM Courses_Lectures " +
                $"WHERE CourseID = {courseID}", connection);

            var lecturers = new List<Lecturer>();

            using (var reader = getLecturersIDCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var lecturerID = reader.GetInt32(0);

                    SqlCommand getLecturersCommand = new SqlCommand($"SELECT LecturerID, Name, BirthDate " +
                        $"FROM Courses WHERE LecturerID = {lecturerID}", connection);

                    using (var subreader = getLecturersCommand.ExecuteReader())
                    {
                        subreader.Read();

                        var lecturer = GetLecturer(subreader);

                        lecturers.Add(lecturer);
                    }
                }
            }

            return lecturers;
        }

        public static List<Course> GetAllCoursesForLecturer(SqlConnection connection, int lecturerID)
        {
            SqlCommand getCoursesIDCommand = new SqlCommand($"SELECT CourseID FROM Courses_Lectures " +
                $"WHERE LecturerID = {lecturerID}", connection);

            var courses = new List<Course>();

            using (var reader = getCoursesIDCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var courseID = reader.GetInt32(0);

                    SqlCommand getCoursesCommand = new SqlCommand($"SELECT CourseID, Name, StartDate, EndDate, " +
                        $"PassingScore FROM Courses WHERE CourseID = {courseID}", connection);

                    using (var subreader = getCoursesCommand.ExecuteReader())
                    {
                        subreader.Read();

                        var course = GetCourse(subreader);

                        courses.Add(course);
                    }
                }
            }

            return courses;
        }
    }
}

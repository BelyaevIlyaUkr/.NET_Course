using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HomeTask5
{
    class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Github { get; set; }
    }

    class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PassingScore { get; set; }
    }

    class Lecturer
    {
        public int LecturerID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    class HomeTask
    {
        public int HomeTaskID { get; set; }
        public string Name { get; set; }
        public string Describing { get; set; }
        public DateTime TaskDate { get; set; }
        public int SerialNumber { get; set; }
        public int CourseID { get; set; }
    }

    class Grade
    {

    }

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
            SqlCommand command = new SqlCommand("SELECT CourseID, Name, StartDate, EndDate, PassingScore FROM Students", connection);

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

        private static HomeTask GetHomeTask(SqlDataReader reader)
        {
            var homeTaskID = reader.GetInt32(0);
            var name = reader.GetString(1);
            var describing = reader.GetString(2);
            var taskDate = reader.GetDateTime(3);
            var serialNumber = reader.GetInt32(4);
            var courseID = reader.GetInt32(5);

            return new HomeTask { HomeTaskID = homeTaskID, Name = name, Describing = describing, 
                TaskDate = taskDate, SerialNumber = serialNumber, CourseID = courseID };
        }

        public static List<HomeTask> GetAllHomeTasks(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT HomeTaskID, Name, Describing, TaskDate" +
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
            var createCommand = new SqlCommand("INSERT INTO HomeTasks (Name, Describing, TaskDate," +
                "SerialNumber, CourseID) VALUES (@name, @describing, @taskDate, @serialNumber, @courseID)", connection);

            createCommand.Parameters.AddWithValue("@name", hometask.Name);
            createCommand.Parameters.AddWithValue("@describing", hometask.Describing);
            createCommand.Parameters.AddWithValue("@taskDate", hometask.TaskDate);
            createCommand.Parameters.AddWithValue("@serialNumber", hometask.SerialNumber);
            createCommand.Parameters.AddWithValue("@courseID", hometask.CourseID);

            createCommand.ExecuteNonQuery();

            return hometask;
        }

        public static void UpdateHomeTask(SqlConnection connection, HomeTask hometask)
        {
            var updateCommand = new SqlCommand("UPDATE Hometasks SET Name = @name," +
            "Describing = @describing, TaskDate = @taskDate, SerialNumber = @serialNumber," +
            "CourseID = courseID WHERE HomeTaskID = @hometaskID", connection);

            updateCommand.Parameters.AddWithValue("@hometaskID", hometask.HomeTaskID);
            updateCommand.Parameters.AddWithValue("@name", hometask.Name);
            updateCommand.Parameters.AddWithValue("@describing", hometask.Describing);
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            //string sCreateDatabase = "CREATE DATABASE StudyManager";

            /*string sCreateTable = "CREATE TABLE Grades(GradeID INTEGER PRIMARY KEY IDENTITY," +
            "GradeDate DATE NOT NULL, IsComplete BIT NOT NULL," +
            "HomeTaskID INT NOT NULL FOREIGN KEY REFERENCES HomeTasks(HomeTaskID) ON DELETE CASCADE," +
            "StudentID INT NOT NULL FOREIGN KEY REFERENCES Students(StudentID) ON DELETE CASCADE)";*/

            /*string sCreateTable = "CREATE TABLE HomeTasks(HomeTaskID INTEGER PRIMARY KEY IDENTITY, Name VARCHAR(80) NOT NULL," +
            "Description TEXT,TaskDate DATE NOT NULL,SerialNumber INT NOT NULL,CourseID INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseID) ON DELETE CASCADE)";*/

            /*string sCreateTable = "CREATE TABLE Students_Courses (StudentID INT NOT NULL FOREIGN KEY " +
            "REFERENCES Students(StudentID) ON DELETE CASCADE," +
            "CourseID INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseID) ON DELETE CASCADE," +
            "PRIMARY KEY (StudentID,CourseID), UNIQUE(StudentID,CourseID))";*/

            /*string sCreateTable = "CREATE TABLE Courses_Lecturers (CourseID INT NOT NULL FOREIGN KEY " +
            "REFERENCES Courses(CourseID) ON DELETE CASCADE," +
            "LecturerID INT NOT NULL FOREIGN KEY REFERENCES Lecturers(LecturerID) ON DELETE CASCADE," +
            "PRIMARY KEY (CourseID,LecturerID), UNIQUE(CourseID,LecturerID))";


            SqlConnection mycon = new SqlConnection();
            mycon.ConnectionString = "Data Source=ILYABELYAEV2A78; Initial Catalog = StudyManager; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            SqlCommand mycomm = new SqlCommand();
            mycomm.CommandType = CommandType.Text;
            mycomm.Connection = mycon;
            mycomm.CommandText = sCreateTable;

            try
            {
                mycon.Open();
                mycomm.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mycon.Close();
            }*/

            using (var connection = new SqlConnection("Data Source=ILYABELYAEV2A78; Initial Catalog = StudyManager; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"))
            {
                connection.Open();

                var student = new Student { FirstName = "Ilya", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };

                Repository.CreateStudent(connection,student);
                
                var students = Repository.GetAllStudents(connection);

                foreach(var stud in students)
                {
                    Console.WriteLine(stud.FirstName);
                }

            }

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HomeTask5
{
    class Program
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

        }

        class Lecturer
        {

        }

        class HomeTask
        {

        }

        class Grade
        {

        }

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

                CreateStudent(connection,student);
                
                var students = GetStudents(connection);

                foreach(var stud in students)
                {
                    Console.WriteLine(stud.FirstName);
                }

            }

            Console.ReadKey();
        }

        
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

        
        private static IEnumerable<Student> GetStudents(SqlConnection connection)
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


        private static Student CreateStudent(SqlConnection connection, Student student)
        {
            var createCommand = new SqlCommand("INSERT INTO Students (FirstName,LastName,PhoneNumber,Email,Github)" +
            "VALUES (@firstName,@lastName,@phoneNumber,@email,@github)",connection);

            createCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            createCommand.Parameters.AddWithValue("@lastName", student.LastName);
            createCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            createCommand.Parameters.AddWithValue("@email", student.Email);
            createCommand.Parameters.AddWithValue("@github", student.Github);

            createCommand.ExecuteNonQuery();
            
            return student;
        }
    }
}

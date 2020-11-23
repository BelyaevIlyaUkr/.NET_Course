using System;
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
        public string Description { get; set; }
        public DateTime TaskDate { get; set; }
        public int SerialNumber { get; set; }
        public int CourseID { get; set; }
    }

    class Grade
    {
        public int GradeID { get; set; } 
        public DateTime GradeDate { get; set; }
        public int IsComplete { get; set; }
        public int HomeTaskID { get; set; }
        public int StudentID { get; set; }
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

                //Repository.DeleteAllCourses(connection);
                //Repository.DeleteAllStudents(connection);
                //Repository.DeleteAllLecturers(connection);

                var student1 = new Student { FirstName = "Ilya", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };
                var student2 = new Student { FirstName = "Konstantin", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };
                var student3 = new Student { FirstName = "Fedor", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };

                var course1 = new Course { Name = ".NET", StartDate = new DateTime(2020, 8, 18), EndDate = new DateTime(2020, 10, 18), PassingScore = 185 };
                var course2 = new Course { Name = "Python", StartDate = new DateTime(2020, 2, 20), EndDate = new DateTime(2020, 5, 20), PassingScore = 150 };
                var course3 = new Course { Name = "C++", StartDate = new DateTime(2018, 3, 15), EndDate = new DateTime(2018, 7, 15), PassingScore = 180 };

                var lecturer1 = new Lecturer { Name = "Alan", BirthDate = new DateTime(1980, 5, 4) };
                var lecturer2 = new Lecturer { Name = "John", BirthDate = new DateTime(1979, 5, 4) };
                var lecturer3 = new Lecturer { Name = "George", BirthDate = new DateTime(1995, 5, 4) };
                
                var hometask1 = new HomeTask { Name = "Hometask1", Description = "Hi", TaskDate = new DateTime(2020, 4, 15), SerialNumber = 1, CourseID = 7 };
                var hometask2 = new HomeTask { Name = "Hometask2", Description = "Good Bye", TaskDate = new DateTime(2020, 3, 17), SerialNumber = 1, CourseID = 8 };
                var hometask3 = new HomeTask { Name = "Hometask3", Description = "Hello", TaskDate = new DateTime(2020, 6, 25), SerialNumber = 1, CourseID = 9 };

                var grade1 = new Grade { GradeDate = new DateTime(2020, 3, 16), IsComplete = 1, HomeTaskID = 1, StudentID = 2 };
                var grade2 = new Grade { GradeDate = new DateTime(2020, 2, 18), IsComplete = 1, HomeTaskID = 2, StudentID = 3 };
                var grade3 = new Grade { GradeDate = new DateTime(2020, 1, 20), IsComplete = 1, HomeTaskID = 3, StudentID = 4 };


                /*Repository.CreateStudent(connection,student1);
                Repository.CreateStudent(connection, student2);
                Repository.CreateStudent(connection, student3);

                Repository.CreateCourse(connection, course1);
                Repository.CreateCourse(connection, course2);
                Repository.CreateCourse(connection, course3);

                Repository.CreateLecturer(connection, lecturer1);
                Repository.CreateLecturer(connection, lecturer2);
                Repository.CreateLecturer(connection, lecturer3);*/

                Repository.CreateHomeTask(connection, hometask1);
                Repository.CreateHomeTask(connection, hometask2);
                Repository.CreateHomeTask(connection, hometask3);

                Repository.CreateGrade(connection, grade1);
                Repository.CreateGrade(connection, grade2);
                Repository.CreateGrade(connection, grade3);


                var courses = Repository.GetAllCourses(connection);
                var students = Repository.GetAllStudents(connection);
                var lecturers = Repository.GetAllLecturers(connection);

                foreach(var stud in students)
                {
                    Console.WriteLine(stud.FirstName);
                }

                Console.WriteLine("");

                foreach(var course in courses)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("");

                foreach(var lecturer in lecturers)
                {
                    Console.WriteLine(lecturer.Name);
                }

            }

            Console.ReadKey();
        }
    }
}

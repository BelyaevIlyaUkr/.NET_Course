using System;
using System.Data.SqlClient;
using System.Globalization;

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
        public bool IsComplete { get; set; }
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

                var grade1 = new Grade { GradeDate = new DateTime(2020, 3, 16), IsComplete = true, HomeTaskID = 4, StudentID = 8 };
                var grade2 = new Grade { GradeDate = new DateTime(2020, 2, 18), IsComplete = true, HomeTaskID = 5, StudentID = 9 };
                var grade3 = new Grade { GradeDate = new DateTime(2020, 1, 20), IsComplete = true, HomeTaskID = 6, StudentID = 10 };


                /*Repository.CreateStudent(connection,student1);
                Repository.CreateStudent(connection, student2);
                Repository.CreateStudent(connection, student3);

                Repository.CreateCourse(connection, course1);
                Repository.CreateCourse(connection, course2);
                Repository.CreateCourse(connection, course3);

                Repository.CreateLecturer(connection, lecturer1);
                Repository.CreateLecturer(connection, lecturer2);
                Repository.CreateLecturer(connection, lecturer3);*/

                /*Repository.CreateHomeTask(connection, hometask1);
                Repository.CreateHomeTask(connection, hometask2);
                Repository.CreateHomeTask(connection, hometask3);*/

                /*Repository.CreateGrade(connection, grade1);
                Repository.CreateGrade(connection, grade2);
                Repository.CreateGrade(connection, grade3);*/


                var courses = Repository.GetAllCourses(connection);
                var students = Repository.GetAllStudents(connection);
                var lecturers = Repository.GetAllLecturers(connection);
                var grades = Repository.GetAllGrades(connection);
                var hometasks = Repository.GetAllHomeTasks(connection);

                Console.WriteLine("All students\n");
                
                foreach (var stud in students)
                {
                    Console.WriteLine(stud.FirstName);
                }

                Console.WriteLine("\nAll courses\n");

                foreach(var course in courses)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("\nAll lecturers\n");

                foreach(var lecturer in lecturers)
                {
                    Console.WriteLine(lecturer.Name);
                }

                Console.WriteLine("\nAll grades\n");

                foreach(var grade in grades)
                {
                    Console.WriteLine(grade.GradeDate.Date.ToString("d", new CultureInfo("fr-FR")));
                }

                Console.WriteLine("\nAll hometasks\n");

                foreach (var hometask in hometasks)
                {
                    Console.WriteLine(hometask.Name);
                }

                /*Repository.CreateStudentCourse(connection, (8, 7));
                Repository.CreateStudentCourse(connection, (8, 8));
                Repository.CreateStudentCourse(connection, (9, 9));
                Repository.CreateStudentCourse(connection, (10,8));
                Repository.CreateStudentCourse(connection, (10, 9));*/

                var coursesForStudent = Repository.GetAllCoursesForStudent(connection, 8);

                Console.WriteLine("\nAll courses for student with ID = 8\n");

                foreach (var course in coursesForStudent)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("\nAll students in course with ID = 9\n");

                var studentsInCourse = Repository.GetAllStudentsInCourse(connection, 9);

                foreach(var student in studentsInCourse)
                {
                    Console.WriteLine(student.FirstName);
                }

                /*Repository.CreateCourseLecturer(connection, (7, 8));
                Repository.CreateCourseLecturer(connection, (7, 9));
                Repository.CreateCourseLecturer(connection, (8, 8));
                Repository.CreateCourseLecturer(connection, (9, 7));
                Repository.CreateCourseLecturer(connection, (9, 9));*/

                Console.WriteLine("\nAll courses with lecturer with ID = 7\n");

                var coursesWithDefiniteLecturer = Repository.GetAllCoursesWithDefiniteLecturer(connection, 7);

                foreach(var course in coursesWithDefiniteLecturer)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("\nAll lecturers for course with ID = 9\n");

                var lecturersForCourse = Repository.GetAllLecturersForCourse(connection, 9);

                foreach(var lecturer in lecturersForCourse)
                {
                    Console.WriteLine(lecturer.Name);
                }

                var firstStudentUpdated = new Student { StudentID = 8, FirstName = "Ivan", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };
                var firstCourseUpdated = new Course { CourseID = 7, Name = "C#", StartDate = new DateTime(2020, 8, 18), EndDate = new DateTime(2020, 10, 18), PassingScore = 185 };
                var firstLecturerUpdated = new Lecturer { LecturerID = 7, Name = "Platon", BirthDate = new DateTime(1980, 5, 4) };
                var firstGradeUpdated = new Grade { GradeID = 8, GradeDate = new DateTime(2020, 3, 24), IsComplete = true, HomeTaskID = 4, StudentID = 8 };
                var firstHomeTaskUpdated = new HomeTask { HomeTaskID = 3, Name = "HometaskUpdated", Description = "Hi", TaskDate = new DateTime(2020, 4, 15), SerialNumber = 1, CourseID = 7 };

                Repository.UpdateCourse(connection, firstCourseUpdated);
                Repository.DeleteCourse(connection, 9);
                Repository.UpdateStudent(connection, firstStudentUpdated);
                Repository.DeleteStudent(connection, 9);
                Repository.UpdateLecturer(connection, firstLecturerUpdated);
                Repository.DeleteLecturer(connection, 8);
                Repository.UpdateHomeTask(connection, firstHomeTaskUpdated);
                Repository.DeleteHomeTask(connection, 5);
                Repository.UpdateGrade(connection, firstGradeUpdated);
                Repository.DeleteGrade(connection, 10);

                Console.WriteLine("After Updating and Deleting");

                courses = Repository.GetAllCourses(connection);
                students = Repository.GetAllStudents(connection);
                lecturers = Repository.GetAllLecturers(connection);
                grades = Repository.GetAllGrades(connection);
                hometasks = Repository.GetAllHomeTasks(connection);

                Console.WriteLine("\nAll students\n");

                foreach (var stud in students)
                {
                    Console.WriteLine(stud.FirstName);
                }

                Console.WriteLine("\nAll courses\n");

                foreach (var course in courses)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("\nAll lecturers\n");

                foreach (var lecturer in lecturers)
                {
                    Console.WriteLine(lecturer.Name);
                }

                Console.WriteLine("\nAll grades\n");

                foreach (var grade in grades)
                {
                    Console.WriteLine(grade.GradeDate.Date.ToString("d", new CultureInfo("fr-FR")));
                }

                Console.WriteLine("\nAll hometasks\n");

                foreach (var hometask in hometasks)
                {
                    Console.WriteLine(hometask.Name);
                }

                Console.WriteLine("\nAll pairs Student-Course\n");

                var studentsCourses = Repository.GetAllStudentsCourses(connection);

                foreach(var studentCourse in studentsCourses)
                {
                    Console.WriteLine(studentCourse);
                }

                Console.WriteLine("\nAll pairs Course-Lecturer\n");

                var coursesLecturers = Repository.GetAllCoursesLecturers(connection);

                foreach(var courseLecturer in coursesLecturers)
                {
                    Console.WriteLine(courseLecturer);
                }


                Repository.DeleteCourseLecturer(connection, (7, 9));
                Repository.DeleteStudentCourse(connection, (8, 7));

                Console.WriteLine("\nAll pairs Student-Course after deleting\n");

                studentsCourses = Repository.GetAllStudentsCourses(connection);

                foreach (var studentCourse in studentsCourses)
                {
                    Console.WriteLine(studentCourse);
                }

                Console.WriteLine("\nAll pairs Course-Lecturer after deleting\n");

                coursesLecturers = Repository.GetAllCoursesLecturers(connection);

                foreach (var courseLecturer in coursesLecturers)
                {
                    Console.WriteLine(courseLecturer);
                }

            }

            Console.ReadKey();
        }
    }
}

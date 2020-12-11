using System;
using System.Data.SqlClient;
using System.Globalization;
using StudyManager.Models;
using StudyManager.DataAccess.ADO;
using System.Threading.Tasks;

namespace HomeTask7
{
    class Program
    {
        static async Task Main(string[] args)
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

            using (var connection = new SqlConnection("Data Source=ILYABELYAEV2A78; Initial Catalog = StudyManager; " +
                "Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; " +
                "ApplicationIntent = ReadWrite; MultiSubnetFailover = False; MultipleActiveResultSets = True"))
            {
                connection.Open();

                //var deleteAllCoursesTask = Repository.DeleteAllCoursesAsync(connection);
                //var deleteAllStudentsTask = Repository.DeleteAllStudentsAsync(connection);
                //var deleteAllLecturersTask = Repository.DeleteAllLecturersAsync(connection);
                //var deleteAllHometaskTask = Repository.DeleteAllHomeTasksAsync(connection);
                //var deleteAllGradesTask = Repository.DeleteAllGradesAsync(connection);

                var student1 = new Student { FirstName = "Ilya", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };
                var student2 = new Student { FirstName = "Konstantin", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };
                var student3 = new Student { FirstName = "Fedor", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };

                var course1 = new Course { Name = ".NET", StartDate = new DateTime(2020, 8, 18), EndDate = new DateTime(2020, 10, 18), PassingScore = 185 };
                var course2 = new Course { Name = "Python", StartDate = new DateTime(2020, 2, 20), EndDate = new DateTime(2020, 5, 20), PassingScore = 150 };
                var course3 = new Course { Name = "C++", StartDate = new DateTime(2018, 3, 15), EndDate = new DateTime(2018, 7, 15), PassingScore = 180 };

                var lecturer1 = new Lecturer { Name = "Alan", BirthDate = new DateTime(1980, 5, 4) };
                var lecturer2 = new Lecturer { Name = "John", BirthDate = new DateTime(1979, 5, 4) };
                var lecturer3 = new Lecturer { Name = "George", BirthDate = new DateTime(1995, 5, 4) };

                var hometask1 = new HomeTask { Name = "Hometask1", Description = "Hi", TaskDate = new DateTime(2020, 4, 15), SerialNumber = 1, CourseID = 20 };
                var hometask2 = new HomeTask { Name = "Hometask2", Description = "Good Bye", TaskDate = new DateTime(2020, 3, 17), SerialNumber = 1, CourseID = 21 };
                var hometask3 = new HomeTask { Name = "Hometask3", Description = "Hello", TaskDate = new DateTime(2020, 6, 25), SerialNumber = 1, CourseID = 22 };

                var grade1 = new Grade { GradeDate = new DateTime(2020, 3, 16), IsComplete = true, HomeTaskID = 37, StudentID = 21 };
                var grade2 = new Grade { GradeDate = new DateTime(2020, 2, 18), IsComplete = true, HomeTaskID = 38, StudentID = 22 };
                var grade3 = new Grade { GradeDate = new DateTime(2020, 1, 20), IsComplete = true, HomeTaskID = 39, StudentID = 23 };

                //Task.WaitAll(new Task[3] { deleteAllCoursesTask, deleteAllStudentsTask, deleteAllLecturersTask });

                /*Task.WaitAll(new Task[] {
                    Repository.CreateStudentAsync(connection, student1),
                    Repository.CreateStudentAsync(connection, student2),
                    Repository.CreateStudentAsync(connection, student3),

                    Repository.CreateCourseAsync(connection, course1),
                    Repository.CreateCourseAsync(connection, course2),
                    Repository.CreateCourseAsync(connection, course3),

                    Repository.CreateLecturerAsync(connection, lecturer1),
                    Repository.CreateLecturerAsync(connection, lecturer2),
                    Repository.CreateLecturerAsync(connection, lecturer3),

                    Repository.CreateHomeTaskAsync(connection, hometask1),
                    Repository.CreateHomeTaskAsync(connection, hometask2),
                    Repository.CreateHomeTaskAsync(connection, hometask3),

                    Repository.CreateGradeAsync(connection, grade1),
                    Repository.CreateGradeAsync(connection, grade2),
                    Repository.CreateGradeAsync(connection, grade3)
                });*/


                var coursesTask = Repository.GetAllCoursesAsync(connection);
                var studentsTask = Repository.GetAllStudentsAsync(connection);
                var lecturersTask = Repository.GetAllLecturersAsync(connection);
                var gradesTask = Repository.GetAllGradesAsync(connection);
                var hometasksTask = Repository.GetAllHomeTasksAsync(connection);

                Console.WriteLine("All students\n");

                foreach (var stud in studentsTask.Result)
                {
                    Console.WriteLine(stud.FirstName);
                }

                Console.WriteLine("\nAll courses\n");

                foreach (var course in coursesTask.Result)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("\nAll lecturers\n");

                foreach (var lecturer in lecturersTask.Result)
                {
                    Console.WriteLine(lecturer.Name);
                }

                Console.WriteLine("\nAll grades\n");

                foreach (var grade in gradesTask.Result)
                {
                    Console.WriteLine(grade.GradeDate.Date.ToString("d", new CultureInfo("fr-FR")));
                }

                Console.WriteLine("\nAll hometasks\n");

                foreach (var hometask in hometasksTask.Result)
                {
                    Console.WriteLine(hometask.Name);
                }
                /*
                Task.WaitAll(new Task[5] {
                    Repository.CreateStudentCourseAsync(connection, (21, 20)),
                    Repository.CreateStudentCourseAsync(connection, (22, 24)),
                    Repository.CreateStudentCourseAsync(connection, (23, 25)),
                    Repository.CreateStudentCourseAsync(connection, (25, 21)),
                    Repository.CreateStudentCourseAsync(connection, (26, 26))
                });

                var coursesForStudentTask = Repository.GetAllCoursesForStudentAsync(connection, 21);
                var studentsInCourseTask = Repository.GetAllStudentsInCourseAsync(connection, 25);

                Console.WriteLine("\nAll courses for student with ID = 21\n");

                foreach (var course in coursesForStudentTask.Result)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("\nAll students in course with ID = 25\n");

                foreach (var student in studentsInCourseTask.Result)
                {
                    Console.WriteLine(student.FirstName);
                }

                Task.WaitAll(new Task[5] {
                    Repository.CreateCourseLecturerAsync(connection, (20, 21)),
                    Repository.CreateCourseLecturerAsync(connection, (21, 22)),
                    Repository.CreateCourseLecturerAsync(connection, (21, 24)),
                    Repository.CreateCourseLecturerAsync(connection, (22, 21)),
                    Repository.CreateCourseLecturerAsync(connection, (24, 24))
                });

                var coursesWithDefiniteLecturerTask = Repository.GetAllCoursesWithDefiniteLecturerAsync(connection, 22);
                var lecturersForCourseTask = Repository.GetAllLecturersForCourseAsync(connection, 21);

                Console.WriteLine("\nAll courses with lecturer with ID = 22\n");

                foreach (var course in coursesWithDefiniteLecturerTask.Result)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("\nAll lecturers for course with ID = 21\n");

                foreach (var lecturer in lecturersForCourseTask.Result)
                {
                    Console.WriteLine(lecturer.Name);
                }
                
                var firstStudentUpdated = new Student { StudentID = 8, FirstName = "Ivan", LastName = "Belyaev", PhoneNumber = "0734552435", Email = "belyaev.i2000@gmail.com", Github = "BelyaevIlyaUkr" };
                var firstCourseUpdated = new Course { CourseID = 7, Name = "C#", StartDate = new DateTime(2020, 8, 18), EndDate = new DateTime(2020, 10, 18), PassingScore = 185 };
                var firstLecturerUpdated = new Lecturer { LecturerID = 7, Name = "Platon", BirthDate = new DateTime(1980, 5, 4) };
                var firstGradeUpdated = new Grade { GradeID = 8, GradeDate = new DateTime(2020, 3, 24), IsComplete = true, HomeTaskID = 4, StudentID = 8 };
                var firstHomeTaskUpdated = new HomeTask { HomeTaskID = 3, Name = "HometaskUpdated", Description = "Hi", TaskDate = new DateTime(2020, 4, 15), SerialNumber = 1, CourseID = 7 };

                Task.WaitAll(new Task[] {
                    Repository.UpdateCourseAsync(connection, firstCourseUpdated),
                    Repository.UpdateStudentAsync(connection, firstStudentUpdated),
                    Repository.UpdateLecturerAsync(connection, firstLecturerUpdated),
                    Repository.UpdateHomeTaskAsync(connection, firstHomeTaskUpdated),
                    Repository.UpdateGradeAsync(connection, firstGradeUpdated)
                });

                Task.WaitAll(new Task[] {
                    Repository.DeleteCourseAsync(connection, 9),
                    Repository.DeleteStudentAsync(connection, 9),
                    Repository.DeleteLecturerAsync(connection, 8),
                    Repository.DeleteHomeTaskAsync(connection, 5),
                    Repository.DeleteGradeAsync(connection, 10)
                });

                Console.WriteLine("After Updating and Deleting");

                coursesTask = Repository.GetAllCoursesAsync(connection);
                studentsTask = Repository.GetAllStudentsAsync(connection);
                lecturersTask = Repository.GetAllLecturersAsync(connection);
                gradesTask = Repository.GetAllGradesAsync(connection);
                hometasksTask = Repository.GetAllHomeTasksAsync(connection);

                Console.WriteLine("\nAll students\n");

                foreach (var stud in studentsTask.Result)
                {
                    Console.WriteLine(stud.FirstName);
                }

                Console.WriteLine("\nAll courses\n");

                foreach (var course in coursesTask.Result)
                {
                    Console.WriteLine(course.Name);
                }

                Console.WriteLine("\nAll lecturers\n");

                foreach (var lecturer in lecturersTask.Result)
                {
                    Console.WriteLine(lecturer.Name);
                }

                Console.WriteLine("\nAll grades\n");

                foreach (var grade in gradesTask.Result)
                {
                    Console.WriteLine(grade.GradeDate.Date.ToString("d", new CultureInfo("fr-FR")));
                }

                Console.WriteLine("\nAll hometasks\n");

                foreach (var hometask in hometasksTask.Result)
                {
                    Console.WriteLine(hometask.Name);
                }

                var studentsCourses = Repository.GetAllStudentsCoursesAsync(connection);
                var coursesLecturers = Repository.GetAllCoursesLecturersAsync(connection);

                Console.WriteLine("\nAll pairs Student-Course\n");

                foreach (var studentCourse in studentsCourses.Result)
                {
                    Console.WriteLine(studentCourse);
                }

                Console.WriteLine("\nAll pairs Course-Lecturer\n");


                foreach (var courseLecturer in coursesLecturers.Result)
                {
                    Console.WriteLine(courseLecturer);
                }


                var deleteCourseLecturerTask = Repository.DeleteCourseLecturerAsync(connection, (7, 9));
                await Repository.DeleteStudentCourseAsync(connection, (8, 7));

                Console.WriteLine("\nAll pairs Student-Course after deleting\n");

                studentsCourses = Repository.GetAllStudentsCoursesAsync(connection);

                foreach (var studentCourse in studentsCourses.Result)
                {
                    Console.WriteLine(studentCourse);
                }

                await deleteCourseLecturerTask;

                Console.WriteLine("\nAll pairs Course-Lecturer after deleting\n");

                coursesLecturers = Repository.GetAllCoursesLecturersAsync(connection);

                foreach (var courseLecturer in coursesLecturers.Result)
                {
                    Console.WriteLine(courseLecturer);
                }*/

            }

            Console.ReadKey();
        }
    }
}

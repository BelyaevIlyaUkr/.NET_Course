﻿using StudyManager.Models;
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

            await createCommand.ExecuteNonQueryAsync();
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


        public static async Task<int> DeleteAllStudentsCoursesAsync(SqlConnection connection)
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
                courses.Add(await CoursesRepository.GetDefiniteCourse(connection, courseID));
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
                students.Add(await StudentsRepository.GetDefiniteStudent(connection, studentID));
            }

            return students;
        }

        public static async Task<List<Dictionary<string, int>>> GetAllStudentsCoursesAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT StudentID, CourseID FROM Students_Courses", connection);

            var studentsCourses = new List<Dictionary<string, int>>();

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
    }
}

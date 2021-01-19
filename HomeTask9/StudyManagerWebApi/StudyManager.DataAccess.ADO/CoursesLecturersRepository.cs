﻿using StudyManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudyManager.DataAccess.ADO
{
    class CoursesLecturersRepository
    {
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
                lecturers.Add(await LecturersRepository.GetDefiniteLecturer(connection, lecturerID));
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
                courses.Add(await CoursesRepository.GetDefiniteCourse(connection, courseID));
            }

            return courses;
        }
    }
}
using StudyManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudyManager.DataAccess.ADO
{
    public class GradesRepository
    {
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
            try
            {
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
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
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
                throw new Exception("Error: there isn't hometask with such ID or/and student with such ID in database");
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
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
                throw new Exception("Error: there isn't hometask or/and student with such ID in database");
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
        }

        public static async Task<int> DeleteGradeAsync(SqlConnection connection, int gradeID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Grades WHERE GradeID = @gradeID", connection);

            deleteCommand.Parameters.AddWithValue("@gradeID", gradeID);

            try
            {
                var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
        }


        public static async Task<int> DeleteAllGradesAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Grades", connection);

            try
            {
                var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (Exception)
            {
                throw new Exception("Error: something wen wrong");
            }
        }

        public static async Task<Grade> GetDefiniteGrade(SqlConnection connection, int gradeID)
        {
            SqlCommand getGradeCommand = new SqlCommand("SELECT GradeID, GradeDate, IsComplete, HomeTaskID," +
                $"StudentID FROM Grades WHERE GradeID = {gradeID}", connection);

            try
            {
                using (var reader = await getGradeCommand.ExecuteReaderAsync())
                {
                    if (reader.Read())
                        return GetGrade(reader);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }

            return null;
        }
    }
}

using StudyManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudyManager.DataAccess.ADO
{
    public class LecturersRepository
    {
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

            try
            {
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
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
        }

        public static async Task<Lecturer> GetDefiniteLecturer(SqlConnection connection, int lecturerID)
        {
            SqlCommand getLecturerCommand = new SqlCommand($"SELECT LecturerID, Name, BirthDate " +
                        $"FROM Lecturers WHERE LecturerID = {lecturerID}", connection);

            try
            {
                using (var reader = await getLecturerCommand.ExecuteReaderAsync())
                {
                    if (reader.Read())
                        return GetLecturer(reader);
                }

                return null;
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
        }

        public static async Task CreateLecturerAsync(SqlConnection connection, Lecturer lecturer)
        {
            var createCommand = new SqlCommand("INSERT INTO Lecturers (Name, BirthDate)" +
            "VALUES (@name, @birthDate)", connection);

            createCommand.Parameters.AddWithValue("@name", lecturer.Name);
            createCommand.Parameters.AddWithValue("@birthDate", lecturer.BirthDate);

            try
            {
                await createCommand.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
        }

        public static async Task<int> UpdateLecturerAsync(SqlConnection connection, Lecturer lecturer)
        {
            var updateCommand = new SqlCommand("UPDATE Lecturers SET Name = @name," +
            "BirthDate = @birthDate WHERE LecturerID = @lecturerID", connection);

            updateCommand.Parameters.AddWithValue("@lecturerID", lecturer.LecturerID);
            updateCommand.Parameters.AddWithValue("@name", lecturer.Name);
            updateCommand.Parameters.AddWithValue("@birthDate", lecturer.BirthDate);

            try
            {
                var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (Exception)
            {
                throw new Exception("Error: something went wrong");
            }
        }

        public static async Task<int> DeleteLecturerAsync(SqlConnection connection, int lecturerID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Lecturers WHERE LecturerID = @lecturerID", connection);

            deleteCommand.Parameters.AddWithValue("@lecturerID", lecturerID);

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

        public static async Task<int> DeleteAllLecturersAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Lecturers", connection);

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
    }
}

using StudyManager.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudyManager.DataAccess.ADO
{
    class LecturersRepository
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

        public static async Task<Lecturer> GetDefiniteLecturer(SqlConnection connection, int lecturerID)
        {
            SqlCommand getLecturerCommand = new SqlCommand($"SELECT LecturerID, Name, BirthDate " +
                        $"FROM Lecturers WHERE LecturerID = {lecturerID}", connection);

            using (var reader = await getLecturerCommand.ExecuteReaderAsync())
            {
                if(reader.Read())
                    return GetLecturer(reader);
            }

            return null;
        }

        public static async Task CreateLecturerAsync(SqlConnection connection, Lecturer lecturer)
        {
            var createCommand = new SqlCommand("INSERT INTO Lecturers (Name, BirthDate)" +
            "VALUES (@name, @birthDate)", connection);

            createCommand.Parameters.AddWithValue("@name", lecturer.Name);
            createCommand.Parameters.AddWithValue("@birthDate", lecturer.BirthDate);

            await createCommand.ExecuteNonQueryAsync();
        }

        public static async Task<int> UpdateLecturerAsync(SqlConnection connection, Lecturer lecturer)
        {
            var updateCommand = new SqlCommand("UPDATE Lecturers SET Name = @name," +
            "BirthDate = @birthDate WHERE LecturerID = @lecturerID", connection);

            updateCommand.Parameters.AddWithValue("@lecturerID", lecturer.LecturerID);
            updateCommand.Parameters.AddWithValue("@name", lecturer.Name);
            updateCommand.Parameters.AddWithValue("@birthDate", lecturer.BirthDate);

            var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteLecturerAsync(SqlConnection connection, int lecturerID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Lecturers WHERE LecturerID = @lecturerID", connection);

            deleteCommand.Parameters.AddWithValue("@lecturerID", lecturerID);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteAllLecturersAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Lecturers", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }
    }
}

using StudyManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudyManager.DataAccess.ADO
{
    public class HometasksRepository
    {
        private static HomeTask GetHomeTask(SqlDataReader reader)
        {
            var homeTaskID = reader.GetInt32(0);
            var name = reader.GetString(1);
            var description = reader.GetString(2);
            var taskDate = reader.GetDateTime(3);
            var serialNumber = reader.GetInt32(4);
            var courseID = reader.GetInt32(5);

            return new HomeTask
            {
                HomeTaskID = homeTaskID,
                Name = name,
                Description = description,
                TaskDate = taskDate,
                SerialNumber = serialNumber,
                CourseID = courseID
            };
        }

        public static async Task<List<HomeTask>> GetAllHomeTasksAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT HomeTaskID, Name, Description, TaskDate, " +
                "SerialNumber, CourseID FROM HomeTasks", connection);

            var homeTasks = new List<HomeTask>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var homeTask = GetHomeTask(reader);
                    homeTasks.Add(homeTask);
                }
            }

            return homeTasks;
        }

        public static async Task CreateHomeTaskAsync(SqlConnection connection, HomeTask hometask)
        {
            var createCommand = new SqlCommand("INSERT INTO HomeTasks (Name, Description, TaskDate," +
                "SerialNumber, CourseID) VALUES (@name, @description, @taskDate, @serialNumber, @courseID)", connection);

            createCommand.Parameters.AddWithValue("@name", hometask.Name);
            createCommand.Parameters.AddWithValue("@description", hometask.Description);
            createCommand.Parameters.AddWithValue("@taskDate", hometask.TaskDate);
            createCommand.Parameters.AddWithValue("@serialNumber", hometask.SerialNumber);
            createCommand.Parameters.AddWithValue("@courseID", hometask.CourseID);

            try
            {
                await createCommand.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                throw new Exception("there isn't course with such ID in database");
            }
        }

        public static async Task<int> UpdateHomeTaskAsync(SqlConnection connection, HomeTask hometask)
        {
            var updateCommand = new SqlCommand("UPDATE Hometasks SET Name = @name," +
            "Description = @description, TaskDate = @taskDate, SerialNumber = @serialNumber," +
            "CourseID = courseID WHERE HomeTaskID = @hometaskID", connection);

            updateCommand.Parameters.AddWithValue("@hometaskID", hometask.HomeTaskID);
            updateCommand.Parameters.AddWithValue("@name", hometask.Name);
            updateCommand.Parameters.AddWithValue("@description", hometask.Description);
            updateCommand.Parameters.AddWithValue("@taskDate", hometask.TaskDate);
            updateCommand.Parameters.AddWithValue("@serialNumber", hometask.SerialNumber);
            updateCommand.Parameters.AddWithValue("@courseID", hometask.CourseID);

            try
            {
                var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

                return numberOfAffectedRows;
            }
            catch (SqlException)
            {
                throw new Exception("There isn't course with such ID in database");
            }
        }

        public static async Task<int> DeleteHomeTaskAsync(SqlConnection connection, int hometaskID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM HomeTasks WHERE HomeTaskID = @hometaskID", connection);

            deleteCommand.Parameters.AddWithValue("@hometaskID", hometaskID);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task<int> DeleteAllHomeTasksAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM HomeTasks", connection);

            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }
    }
}

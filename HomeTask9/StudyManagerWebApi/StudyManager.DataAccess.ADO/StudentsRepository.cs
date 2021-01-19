using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class StudentsRepository
    {
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


        public static async Task<List<Student>> GetAllStudentsAsync(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT StudentID, FirstName, LastName, PhoneNumber, Email, Github FROM Students", connection);

            var students = new List<Student>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var student = GetStudent(reader);
                    students.Add(student);
                }
            }

            return students;
        }

        public static async Task<Student> GetDefiniteStudent(SqlConnection connection, int studentID)
        {
            SqlCommand getStudentCommand = new SqlCommand("SELECT StudentID, FirstName, LastName, " +
                $"PhoneNumber, Email, Github FROM Students WHERE StudentID = {studentID}", connection);

            using (var reader = await getStudentCommand.ExecuteReaderAsync())
            {
                if (reader.Read())
                    return GetStudent(reader);
            }

            return null;
        }

        public static async Task CreateStudentAsync(SqlConnection connection, Student student)
        {
            var createCommand = new SqlCommand("INSERT INTO Students (FirstName,LastName,PhoneNumber,Email,Github)" +
            "VALUES (@firstName,@lastName,@phoneNumber,@email,@github)", connection);

            createCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            createCommand.Parameters.AddWithValue("@lastName", student.LastName);
            createCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            createCommand.Parameters.AddWithValue("@email", student.Email);
            createCommand.Parameters.AddWithValue("@github", student.Github);

            await createCommand.ExecuteNonQueryAsync();
        }


        public static async Task<int> DeleteStudentAsync(SqlConnection connection, int studentID)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students WHERE StudentID = @studentID", connection);

            deleteCommand.Parameters.AddWithValue("@studentID", studentID);
            
            var numberOfAffectedRows = await deleteCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }

        public static async Task DeleteAllStudentsAsync(SqlConnection connection)
        {
            var deleteCommand = new SqlCommand("DELETE FROM Students", connection);

            await deleteCommand.ExecuteNonQueryAsync();
        }

        public static async Task<int> UpdateStudentAsync(SqlConnection connection, Student student)
        {
            var updateCommand = new SqlCommand("UPDATE Students SET FirstName = @firstName," +
            "LastName = @lastName, PhoneNumber = @phoneNumber, Email = @email, Github = @github " +
            "WHERE StudentID = @studentID", connection);

            updateCommand.Parameters.AddWithValue("@studentID", student.StudentID);
            updateCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            updateCommand.Parameters.AddWithValue("@lastName", student.LastName);
            updateCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            updateCommand.Parameters.AddWithValue("@email", student.Email);
            updateCommand.Parameters.AddWithValue("@github", student.Github);

            var numberOfAffectedRows = await updateCommand.ExecuteNonQueryAsync();

            return numberOfAffectedRows;
        }
    }
}

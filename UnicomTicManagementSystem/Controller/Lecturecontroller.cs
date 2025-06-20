using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnicomTicManagementSystem.Model;
using UnicomTicManagementSystem.Repositories;

namespace UnicomTicManagementSystem.Controller
{
    public class Lecturecontroller
    {

        public async Task<bool> AddLecturerAsync(Lecture lecturer)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"INSERT INTO Lecturers (UserID, Name, Address, Gender, Salary, PhoneNumber)
                             VALUES (@UserID, @Name, @Address, @Gender, @Salary, @PhoneNumber)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", lecturer.UserID);
                        cmd.Parameters.AddWithValue("@Name", lecturer.Name);
                        cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                        cmd.Parameters.AddWithValue("@Gender", lecturer.Gender);
                        cmd.Parameters.AddWithValue("@Salary", lecturer.Salary);
                        cmd.Parameters.AddWithValue("@PhoneNumber", lecturer.PhoneNumber);

                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Database error in AddLecturerAsync:\n" + ex.Message);
                return false;
            }
        }

        

        public async Task<bool> UpdateLecturerAsync(Lecture lecturer)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = @"UPDATE Lecturers SET Name = @Name, Address = @Address, 
                                 Gender = @Gender, Salary = @Salary, PhoneNumber = @PhoneNumber 
                                 WHERE UserID = @UserID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", lecturer.UserID);
                    cmd.Parameters.AddWithValue("@Name", lecturer.Name);
                    cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                    cmd.Parameters.AddWithValue("@Gender", lecturer.Gender);
                    cmd.Parameters.AddWithValue("@Salary", lecturer.Salary);
                    cmd.Parameters.AddWithValue("@PhoneNumber", lecturer.PhoneNumber);

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteLecturerAsync(string userId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "DELETE FROM Lecturers WHERE UserID = @UserID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<List<Lecture>> GetAllLecturersAsync()
        {
            var lecturers = new List<Lecture>();

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT * FROM Lecturers";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lecturers.Add(new Lecture
                        {
                            UserID = reader["UserID"].ToString(),
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        });
                    }
                }
            }

            return lecturers;
        }

        public async Task<Lecture> GetLecturerByUserIdAsync(string userId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT * FROM Lecturers WHERE UserID = @UserID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Lecture
                            {
                                UserID = reader["UserID"].ToString(),
                                Name = reader["Name"].ToString(),
                                Address = reader["Address"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                PhoneNumber = reader["PhoneNumber"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        // ✅ Search by Name (partial match)
        public async Task<List<Lecture>> GetLecturerByNameAsync(string name)
        {
            var lecturers = new List<Lecture>();

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT * FROM Lecturers WHERE Name LIKE @Name";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", $"%{name}%");
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lecturers.Add(new Lecture
                            {
                                UserID = reader["UserID"].ToString(),
                                Name = reader["Name"].ToString(),
                                Address = reader["Address"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                PhoneNumber = reader["PhoneNumber"].ToString()
                            });
                        }
                    }
                }
            }

            return lecturers;
        }
    }
}
  
  


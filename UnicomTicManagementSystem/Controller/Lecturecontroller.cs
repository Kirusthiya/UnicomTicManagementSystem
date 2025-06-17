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
                    string query = @"INSERT INTO Lecturers 
                                    (UserID, LecturerName, Address, Gender, Salary, PhoneNumber)
                                     VALUES 
                                    (@UserID, @LecturerName, @Address, @Gender, @Salary, @PhoneNumber)";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", lecturer.UserID);
                        cmd.Parameters.AddWithValue("@LecturerName", lecturer.Name);
                        cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                        cmd.Parameters.AddWithValue("@Gender", lecturer.Gender);
                        cmd.Parameters.AddWithValue("@Salary", lecturer.Salary);
                        cmd.Parameters.AddWithValue("@PhoneNumber", lecturer.PhoneNumber);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding lecturer: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateLecturerAsync(Lecture lecturer)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"UPDATE Lecturers
                                     SET LecturerName = @LecturerName,
                                         Address = @Address,
                                         Gender = @Gender,
                                         Salary = @Salary,
                                         PhoneNumber = @PhoneNumber
                                         WHERE UserID = @UserID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LecturerName", lecturer.Name);
                        cmd.Parameters.AddWithValue("@Address", lecturer.Address);
                        cmd.Parameters.AddWithValue("@Gender", lecturer.Gender);
                        cmd.Parameters.AddWithValue("@Salary", lecturer.Salary);
                        cmd.Parameters.AddWithValue("@PhoneNumber", lecturer.PhoneNumber);
                        cmd.Parameters.AddWithValue("@UserID", lecturer.UserID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating lecturer: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteLecturerAsync(string userId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "DELETE FROM Lecturers WHERE UserID = @UserID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting lecturer: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Lecture>> GetAllLecturersAsync()
        {
            var list = new List<Lecture>();

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT l.UserID, l.LecturerName, l.Address, l.Gender, l.Salary, l.PhoneNumber,
                               u.Username
                        FROM Lecturers l
                        JOIN Users u ON l.UserID = u.UserID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new Lecture
                            {
                                UserID = reader["UserID"].ToString(),
                                Name = reader["LecturerName"].ToString(),
                                Address = reader["Address"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                UserName = reader["Username"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving lecturers: " + ex.Message);
            }

            return list;
        }

        public async Task<List<Lecture>> SearchLecturersByNameAsync(string name)
        {
            var list = new List<Lecture>();

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT l.UserID, l.LecturerName, l.Address, l.Gender, l.Salary, l.PhoneNumber,
                               u.Username
                        FROM Lecturers l
                        JOIN Users u ON l.UserID = u.UserID
                        WHERE l.LecturerName LIKE @Name";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", $"%{name}%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new Lecture
                                {
                                    UserID = reader["UserID"].ToString(),
                                    Name = reader["LecturerName"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    Salary = Convert.ToDecimal(reader["Salary"]),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    UserName = reader["Username"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching lecturers: " + ex.Message);
            }

            return list;
        }
    }
}
 
  


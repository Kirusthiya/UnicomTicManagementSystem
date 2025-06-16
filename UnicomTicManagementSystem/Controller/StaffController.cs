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
    public class StaffController
    {
        public async Task<bool> AddStaffAsync(Staff staff)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"INSERT INTO Staff (UserID, StaffName, Address, Gender, Position, Salary, PhoneNumber)
                                     VALUES (@UserID, @StaffName, @Address, @Gender, @Position, @Salary, @PhoneNumber)";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", staff.UserID);
                        cmd.Parameters.AddWithValue("@StaffName", staff.Name);
                        cmd.Parameters.AddWithValue("@Address", staff.Address);
                        cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                        cmd.Parameters.AddWithValue("@Position", staff.Position);
                        cmd.Parameters.AddWithValue("@Salary", staff.Salary);
                        cmd.Parameters.AddWithValue("@PhoneNumber", staff.PhoneNumber);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding staff: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateStaffAsync(Staff staff)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"UPDATE Staff
                                     SET StaffName = @StaffName, Address = @Address, Gender = @Gender,
                                         Position = @Position, Salary = @Salary, PhoneNumber = @PhoneNumber
                                     WHERE UserID = @UserID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StaffName", staff.Name);
                        cmd.Parameters.AddWithValue("@Address", staff.Address);
                        cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                        cmd.Parameters.AddWithValue("@Position", staff.Position);
                        cmd.Parameters.AddWithValue("@Salary", staff.Salary);
                        cmd.Parameters.AddWithValue("@PhoneNumber", staff.PhoneNumber);
                        cmd.Parameters.AddWithValue("@UserID", staff.UserID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating staff: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteStaffAsync(string userId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "DELETE FROM Staff WHERE UserID = @UserID";

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
                Console.WriteLine("Error deleting staff: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Staff>> GetAllStaffAsync()
        {
            var list = new List<Staff>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT s.UserID, s.StaffName, s.Address, s.Gender, s.Position,
                               s.Salary, s.PhoneNumber, u.Username AS UserName
                        FROM Staff s
                        JOIN Users u ON s.UserID = u.UserID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new Staff
                            {
                                UserID = reader["UserID"].ToString(),
                                Name = reader["StaffName"].ToString(),
                                Address = reader["Address"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Position = reader["Position"].ToString(),
                                Salary = Convert.ToDouble(reader["Salary"]),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                UserName = reader["UserName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving staff list: " + ex.Message);
            }

            return list;
        }

        public async Task<List<Staff>> SearchStaffByNameAsync(string name)
        {
            var list = new List<Staff>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT s.UserID, s.StaffName, s.Address, s.Gender, s.Position,
                               s.Salary, s.PhoneNumber, u.Username AS UserName
                        FROM Staff s
                        JOIN Users u ON s.UserID = u.UserID
                        WHERE s.StaffName LIKE @Name";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", $"%{name}%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new Staff
                                {
                                    UserID = reader["UserID"].ToString(),
                                    Name = reader["StaffName"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    Position = reader["Position"].ToString(),
                                    Salary = Convert.ToDouble(reader["Salary"]),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    UserName = reader["UserName"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching staff: " + ex.Message);
            }

            return list;
        }
    }
}
        


        
    



using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem.Model;
using UnicomTicManagementSystem.Repositories;

namespace UnicomTicManagementSystem.Controller
{
    public class UserController
    {
        //create user id==================================
        public string GenerateUserID(string role)
        {
            string prefix;
            switch (role)
            {
                case "Admin":
                    prefix = "A";
                    break;
                case "Student":
                    prefix = "S";
                    break;
                case "Staff":
                    prefix = "Sf";
                    break;
                case "Lecturer":
                    prefix = "L";
                    break;
                default:
                    prefix = "U";
                    break;
            }

            Random rand = new Random();
            int number = rand.Next(100000, 999999); 
            return prefix + number;
        }

       // create new user ==================================================
        public async Task<bool> CreateUserAsync(User user, string confirmPassword)
        {
            try
            { 
                if (!user.UserName.EndsWith("@gmail.com"))
                {
                    user.UserName += "@gmail.com";
                }
               if (!user.UserName.EndsWith("@gmail.com") || user.UserName.Contains(" "))
                    throw new Exception("Invalid email format. Must be a valid Gmail address.");

               // Password Validation========================================
                if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 8)
                    throw new Exception("Password must be at least 8 characters long.");

                if (user.Password != confirmPassword)
                    throw new Exception("Passwords do not match.");

                user.UserID = GenerateUserID(user.Role);

               //douplicate usename check =========================
                using (var connCheck = DatabaseManager.GetConnection())
                {
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    using (var checkCmd = new SQLiteCommand(checkQuery, connCheck))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", user.UserName);
                        long count = (long)(await checkCmd.ExecuteScalarAsync());
                        if (count > 0)
                            throw new Exception("A user with this email already exists.");
                    }
                }

               //add user in datebase==================================
                using (var connInsert = DatabaseManager.GetConnection())
                {
                    string insertQuery = "INSERT INTO Users (UserID, Username, Password, Role) VALUES (@UserID, @Username, @Password, @Role)";
                    using (var insertCmd = new SQLiteCommand(insertQuery, connInsert))
                    {
                        insertCmd.Parameters.AddWithValue("@UserID", user.UserID);
                        insertCmd.Parameters.AddWithValue("@Username", user.UserName);
                        insertCmd.Parameters.AddWithValue("@Password", user.Password);
                        insertCmd.Parameters.AddWithValue("@Role", user.Role);

                        int rows = await insertCmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("User creation error: " + ex.Message);
                return false;
            }
        }

        //user return =============================================
        public async Task<List<User>> GetAllUsersAsync()
        {
            var userList = new List<User>();

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Users";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userList.Add(new User
                            {
                                UserID = reader["UserID"].ToString(),
                                UserName = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = reader["Role"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching users: " + ex.Message);
            }

            return userList;
        }
        //filter student only================================================
        public async Task<List<User>> GetAllStudentsAsync()
        {
            var allUsers = await GetAllUsersAsync();
            return allUsers.Where(u => u.Role == "Student").ToList();
        }

    }
}
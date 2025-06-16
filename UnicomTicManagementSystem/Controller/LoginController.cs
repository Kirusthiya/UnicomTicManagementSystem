using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem.Model;
using UnicomTicManagementSystem.Repositories;

namespace UnicomTicManagementSystem.Controller
{
    public class LoginController
    {
        // Verify login credentials
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new User
                                {
                                    UserID = reader["UserID"].ToString(),
                                    UserName = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    Role = reader["Role"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login error: " + ex.Message);
            }

            return null; // Invalid login
        }

        // Check if username exists in DB
        public async Task<bool> IsUserExistsAsync(string username)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        long count = (long)await cmd.ExecuteScalarAsync();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Check user exists error: " + ex.Message);
                return false;
            }
        }

        // Update user password
        public async Task<bool> UpdatePasswordAsync(string username, string newPassword)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "UPDATE Users SET Password = @Password WHERE Username = @Username";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        cmd.Parameters.AddWithValue("@Username", username);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update password error: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> IsAnyUSerExitsAsync()
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Users";
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }
    }
}
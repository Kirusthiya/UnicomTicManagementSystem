using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicomTicManagementSystem.Model;
using UnicomTicManagementSystem.Repositories;

namespace UnicomTicManagementSystem.Controller
{
    public class StudentController
    {
        public async Task<bool> AddStudentAsync(Student student)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"INSERT INTO Students (UserID, StudentName, Gender, Address, PhoneNumber, CourseID) VALUES (@UserID, @StudentName, @Gender, @Address, @PhoneNumber, @CourseID)";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", student.UserID);
                        cmd.Parameters.AddWithValue("@StudentName", student.Name);
                        cmd.Parameters.AddWithValue("@Gender", student.Gender);
                        cmd.Parameters.AddWithValue("@Address", student.Address);
                        cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                        cmd.Parameters.AddWithValue("@CourseID", student.CourseID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding student: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        UPDATE Students 
                        SET StudentName = @StudentName, Gender = @Gender, Address = @Address, 
                        PhoneNumber = @PhoneNumber, CourseID = @CourseID 
                        WHERE UserID = @UserID"
                    ;

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentName", student.Name);
                        cmd.Parameters.AddWithValue("@Gender", student.Gender);
                        cmd.Parameters.AddWithValue("@Address", student.Address);
                        cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                        cmd.Parameters.AddWithValue("@CourseID", student.CourseID);
                        cmd.Parameters.AddWithValue("@UserID", student.UserID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating student: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(string userId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "DELETE FROM Students WHERE UserID = @UserID";

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
                Console.WriteLine("Error deleting student: " + ex.Message);
                return false;
            }
        }

        public async Task<Student> GetStudentByUserIdAsync(string userId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Students WHERE UserID = @UserID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Student
                                {
                                    UserID = reader["UserID"].ToString(),
                                    Name = reader["StudentName"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    CourseID = Convert.ToInt32(reader["CourseID"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching student: " + ex.Message);
            }
            return null;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students = new List<Student>();

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Students";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            students.Add(new Student
                            {
                                UserID = reader["UserID"].ToString(),
                                Name = reader["StudentName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Address = reader["Address"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                CourseID = Convert.ToInt32(reader["CourseID"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving students: " + ex.Message);
            }

            return students;
        }
    }
}

    
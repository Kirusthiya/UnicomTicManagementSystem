using Microsoft.Data.Sqlite;
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
    public class CourseController
    {
            public async Task<bool> AddCourseAsync(Course course)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "INSERT INTO Courses (CourseName) VALUES (@CourseName)";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding course: " + ex.Message);
                    return false;
                }
            }
         //update course============================
            public async Task<bool> UpdateCourseAsync(Course course)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "UPDATE Courses SET CourseName = @CourseName WHERE CourseID = @CourseID";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                            cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating course: " + ex.Message);
                    return false;
                }
            }
        //delete course==========================
            public async Task<bool> DeleteCourseAsync(int courseId)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "DELETE FROM Courses WHERE CourseID = @CourseID";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseID", courseId);
                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting course: " + ex.Message);
                    return false;
                }
            }

            public async Task<Course> GetCourseByIdAsync(int courseId)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "SELECT * FROM Courses WHERE CourseID = @CourseID";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseID", courseId);
                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    return new Course
                                    {
                                        CourseID = Convert.ToInt32(reader["CourseID"]),
                                        CourseName = reader["CourseName"].ToString()
                                    };
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching course: " + ex.Message);
                }

                return null;
            }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            var courses = new List<Course>();

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Courses";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            courses.Add(new Course
                            {
                                CourseID = Convert.ToInt32(reader["CourseID"]),
                                CourseName = reader["CourseName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading courses: " + ex.Message);
            }

            return courses;
        }
    }
}







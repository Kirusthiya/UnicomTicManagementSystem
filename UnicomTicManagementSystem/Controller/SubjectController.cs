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
    public class SubjectController
    {
            public async Task<bool> AddSubjectAsync(Subject subject)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"INSERT INTO Subjects (SubjectName, CourseID) VALUES (@SubjectName, @CourseID)";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                            cmd.Parameters.AddWithValue("@CourseID", subject.CourseID);

                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding subject: " + ex.Message);
                    return false;
                }
            }

            public async Task<bool> UpdateSubjectAsync(Subject subject)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"UPDATE Subjects SET SubjectName = @SubjectName, CourseID = @CourseID WHERE SubjectID = @SubjectID";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                            cmd.Parameters.AddWithValue("@CourseID", subject.CourseID);
                            cmd.Parameters.AddWithValue("@SubjectID", subject.SubjectID);

                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating subject: " + ex.Message);
                    return false;
                }
            }

            public async Task<bool> DeleteSubjectAsync(int subjectId)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "DELETE FROM Subjects WHERE SubjectID = @SubjectID";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting subject: " + ex.Message);
                    return false;
                }
            }

            public async Task<Subject> GetSubjectByIdAsync(int subjectId)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "SELECT * FROM Subjects WHERE SubjectID = @SubjectID";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SubjectID", subjectId);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    return new Subject
                                    {
                                        SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                        SubjectName = reader["SubjectName"].ToString(),
                                        CourseID = Convert.ToInt32(reader["CourseID"])
                                    };
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching subject: " + ex.Message);
                }
                return null;
            }

            public async Task<List<Subject>> GetAllSubjectsAsync()
            {
                var subjects = new List<Subject>();

                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "SELECT * FROM Subjects";

                        using (var cmd = new SQLiteCommand(query, conn))
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                subjects.Add(new Subject
                                {
                                    SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                    SubjectName = reader["SubjectName"].ToString(),
                                    CourseID = Convert.ToInt32(reader["CourseID"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving subjects: " + ex.Message);
                }

                return subjects;
            }
        public async Task<List<Subject>> SearchSubjectsAsync(string keyword)
        {
            var subjects = new List<Subject>();

            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                SELECT * FROM Subjects 
                WHERE SubjectName LIKE @Keyword
            ";
                    command.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                SubjectName = reader["SubjectName"].ToString(),
                                CourseID = Convert.ToInt32(reader["CourseID"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching subjects: " + ex.Message);
            }

            return subjects;
        }
    }
 }
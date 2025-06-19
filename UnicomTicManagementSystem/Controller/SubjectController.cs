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
                    string query = @"INSERT INTO Subjects (SubjectName, CourseID)
                                     VALUES (@SubjectName, @CourseID)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                        cmd.Parameters.AddWithValue("@CourseID", subject.CourseID);

                        int result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
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
                    string query = @"UPDATE Subjects 
                                     SET SubjectName = @SubjectName, CourseID = @CourseID
                                     WHERE SubjectID = @SubjectID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                        cmd.Parameters.AddWithValue("@CourseID", subject.CourseID);
                        cmd.Parameters.AddWithValue("@SubjectID", subject.SubjectID);

                        int result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
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
                    string query = @"DELETE FROM Subjects WHERE SubjectID = @SubjectID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                        int result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting subject: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            var subjects = new List<Subject>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT s.SubjectID, s.SubjectName, s.CourseID, c.CourseName
                        FROM Subjects s
                        JOIN Courses c ON s.CourseID = c.CourseID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                SubjectName = reader["SubjectName"].ToString(),
                                CourseID = Convert.ToInt32(reader["CourseID"]),
                                CourseName = reader["CourseName"].ToString()
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

        public async Task<List<Subject>> SearchSubjectsByNameAsync(string name)
        {
            var subjects = new List<Subject>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT s.SubjectID, s.SubjectName, s.CourseID, c.CourseName
                        FROM Subjects s
                        JOIN Courses c ON s.CourseID = c.CourseID
                        WHERE s.SubjectName LIKE @Name";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", $"%{name}%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                subjects.Add(new Subject
                                {
                                    SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                    SubjectName = reader["SubjectName"].ToString(),
                                    CourseID = Convert.ToInt32(reader["CourseID"]),
                                    CourseName = reader["CourseName"].ToString()
                                });
                            }
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
  
     
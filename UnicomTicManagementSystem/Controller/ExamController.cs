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
    public class ExamController
    {
        public async Task<bool> AddExamAsync(Exam exam)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"INSERT INTO Exams (ExamName, SubjectID, CourseID) 
                                     VALUES (@ExamName, @SubjectID, @CourseID)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                        cmd.Parameters.AddWithValue("@SubjectID", exam.SubjectID);
                        cmd.Parameters.AddWithValue("@CourseID", exam.CourseID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding exam: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateExamAsync(Exam exam)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"UPDATE Exams 
                                     SET ExamName = @ExamName, SubjectID = @SubjectID, CourseID = @CourseID 
                                     WHERE ExamID = @ExamID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                        cmd.Parameters.AddWithValue("@SubjectID", exam.SubjectID);
                        cmd.Parameters.AddWithValue("@CourseID", exam.CourseID);
                        cmd.Parameters.AddWithValue("@ExamID", exam.ExamID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating exam: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteExamAsync(int examID)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "DELETE FROM Exams WHERE ExamID = @ExamID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamID", examID);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting exam: " + ex.Message);
                return false;
            }
        }

        public async Task<Exam> GetExamByIdAsync(int examID)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Exams WHERE ExamID = @ExamID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamID", examID);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Exam
                                {
                                    ExamID = Convert.ToInt32(reader["ExamID"]),
                                    ExamName = reader["ExamName"].ToString(),
                                    SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                    CourseID = Convert.ToInt32(reader["CourseID"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching exam: " + ex.Message);
            }

            return null;
        }

        public async Task<List<Exam>> GetAllExamsAsync()
        {
            var exams = new List<Exam>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Exams";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            exams.Add(new Exam
                            {
                                ExamID = Convert.ToInt32(reader["ExamID"]),
                                ExamName = reader["ExamName"].ToString(),
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                CourseID = Convert.ToInt32(reader["CourseID"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading exams: " + ex.Message);
            }

            return exams;
        }

        public async Task<List<Subject>> SearchSubjectsAsync(string keyword)
        {
            var subjects = new List<Subject>();

            try
            {
                using (var connection = DatabaseManager.GetConnection())
                {
                    await connection.OpenAsync();
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
        
    
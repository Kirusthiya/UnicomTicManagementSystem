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
    public class MarkController
    {
            public async Task<bool> AddMarkAsync(Model.Mark mark)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"INSERT INTO Marks (UserID, ExamID, SubjectID, Score)
                                     VALUES (@UserID, @ExamID, @SubjectID, @Score)";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", mark.UserID);
                            cmd.Parameters.AddWithValue("@ExamID", mark.ExamID);
                            cmd.Parameters.AddWithValue("@SubjectID", mark.SubjectID);
                            cmd.Parameters.AddWithValue("@Score", mark.Socre);

                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding mark: " + ex.Message);
                    return false;
                }
            }

            public async Task<bool> UpdateMarkAsync(Model.Mark mark)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"UPDATE Marks 
                                     SET Score = @Score 
                                     WHERE MarkID = @MarkID";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Score", mark.Socre);
                            cmd.Parameters.AddWithValue("@MarkID", mark.MarkID);

                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating mark: " + ex.Message);
                    return false;
                }
            }

            public async Task<bool> DeleteMarkAsync(int markId)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "DELETE FROM Marks WHERE MarkID = @MarkID";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MarkID", markId);
                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting mark: " + ex.Message);
                    return false;
                }
            }

            public async Task<List<Model.Mark>> GetAllMarksAsync()
            {
                var marks = new List<Model.Mark>();
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"
                    SELECT m.MarkID, m.UserID, u.Name AS StudentName, m.ExamID, e.ExamName, 
                           m.SubjectID, s.SubjectName, m.Score 
                    FROM Marks m
                    JOIN Users u ON m.UserID = u.UserID
                    JOIN Exams e ON m.ExamID = e.ExamID
                    JOIN Subjects s ON m.SubjectID = s.SubjectID";

                        using (var cmd = new SQLiteCommand(query, conn))
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                marks.Add(new Model.Mark
                                {
                                    MarkID = Convert.ToInt32(reader["MarkID"]),
                                    UserID =  reader["UserID"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    ExamID = Convert.ToInt32(reader["ExamID"]),
                                    ExamName = reader["ExamName"].ToString(),
                                    SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                    SubjectName = reader["SubjectName"].ToString(),
                                    Socre = Convert.ToInt32(reader["Score"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving marks: " + ex.Message);
                }

                return marks;
            }

            public async Task<List<Model.Mark>> SearchMarksByStudentNameAsync(string name)
            {
                var marks = new List<Model.Mark>();
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"
                    SELECT m.MarkID, m.UserID, u.Name AS StudentName, m.ExamID, e.ExamName, 
                           m.SubjectID, s.SubjectName, m.Score 
                    FROM Marks m
                    JOIN Users u ON m.UserID = u.UserID
                    JOIN Exams e ON m.ExamID = e.ExamID
                    JOIN Subjects s ON m.SubjectID = s.SubjectID
                    WHERE u.Name LIKE @Name";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", $"%{name}%");

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    marks.Add(new Model.Mark
                                    {
                                        MarkID = Convert.ToInt32(reader["MarkID"]),
                                        UserID = reader["UserID"].ToString(),
                                        StudentName = reader["StudentName"].ToString(),
                                        ExamID = Convert.ToInt32(reader["ExamID"]),
                                        ExamName = reader["ExamName"].ToString(),
                                        SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                        SubjectName = reader["SubjectName"].ToString(),
                                        Socre = Convert.ToInt32(reader["Score"])
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error searching marks: " + ex.Message);
                }

                return marks;
            }
        public async Task<List<Model.Mark>> GetMarksByUserIDAsync(string userID)
        {
            var marks = new List<Model.Mark>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT m.MarkID, m.UserID, u.Name AS StudentName, m.ExamID, e.ExamName, 
                               m.SubjectID, s.SubjectName, m.Score 
                        FROM Marks m
                        JOIN Users u ON m.UserID = u.UserID
                        JOIN Exams e ON m.ExamID = e.ExamID
                        JOIN Subjects s ON m.SubjectID = s.SubjectID
                        WHERE m.UserID = @UserID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                marks.Add(new Model.Mark
                                {
                                    MarkID = Convert.ToInt32(reader["MarkID"]),
                                    UserID = reader["UserID"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    ExamID = Convert.ToInt32(reader["ExamID"]),
                                    ExamName = reader["ExamName"].ToString(),
                                    SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                    SubjectName = reader["SubjectName"].ToString(),
                                    Socre = Convert.ToInt32(reader["Score"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving marks by UserID: " + ex.Message);
            }

            return marks;
        }


    }
}

    






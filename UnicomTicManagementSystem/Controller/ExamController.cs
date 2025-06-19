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
        public async Task<List<Exam>> GetAllExamsAsync()
        {
            var exams = new List<Exam>();

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = @"SELECT e.ExamID, e.ExamName, e.SubjectID, e.CourseID, e.FileName,
                                 s.SubjectName, c.CourseName
                                 FROM Exams e
                                 JOIN Subjects s ON e.SubjectID = s.SubjectID
                                 JOIN Courses c ON e.CourseID = c.CourseID";

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
                            CourseID = Convert.ToInt32(reader["CourseID"]),
                            FileName = reader["FileName"].ToString(),
                            SubjectName = reader["SubjectName"].ToString(),
                            CourseName = reader["CourseName"].ToString()
                        });
                    }
                }
            }

            return exams;
        }

        public async Task<bool> AddExamAsync(Exam exam)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = @"INSERT INTO Exams (ExamName, SubjectID, CourseID, FileName) 
                                 VALUES (@ExamName, @SubjectID, @CourseID, @FileName)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                    cmd.Parameters.AddWithValue("@SubjectID", exam.SubjectID);
                    cmd.Parameters.AddWithValue("@CourseID", exam.CourseID);
                    cmd.Parameters.AddWithValue("@FileName", exam.FileName ?? "");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> UpdateExamAsync(Exam exam)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = @"UPDATE Exams 
                                 SET ExamName = @ExamName, SubjectID = @SubjectID, 
                                     CourseID = @CourseID, FileName = @FileName 
                                 WHERE ExamID = @ExamID";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                    cmd.Parameters.AddWithValue("@SubjectID", exam.SubjectID);
                    cmd.Parameters.AddWithValue("@CourseID", exam.CourseID);
                    cmd.Parameters.AddWithValue("@FileName", exam.FileName ?? "");
                    cmd.Parameters.AddWithValue("@ExamID", exam.ExamID);

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteExamAsync(int examID)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "DELETE FROM Exams WHERE ExamID = @ExamID";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ExamID", examID);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<List<Exam>> SearchExamAsync(string keyword)
        {
            var exams = new List<Exam>();

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = @"SELECT e.ExamID, e.ExamName, e.SubjectID, e.CourseID, e.FileName,
                                 s.SubjectName, c.CourseName
                                 FROM Exams e
                                 JOIN Subjects s ON e.SubjectID = s.SubjectID
                                 JOIN Courses c ON e.CourseID = c.CourseID
                                 WHERE e.ExamName LIKE @keyword 
                                 OR s.SubjectName LIKE @keyword 
                                 OR c.CourseName LIKE @keyword";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            exams.Add(new Exam
                            {
                                ExamID = Convert.ToInt32(reader["ExamID"]),
                                ExamName = reader["ExamName"].ToString(),
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                CourseID = Convert.ToInt32(reader["CourseID"]),
                                FileName = reader["FileName"].ToString(),
                                SubjectName = reader["SubjectName"].ToString(),
                                CourseName = reader["CourseName"].ToString()
                            });
                        }
                    }
                }
            }

            return exams;
        }

        public async Task<Dictionary<int, string>> GetCoursesAsync()
        {
            var courses = new Dictionary<int, string>();

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT CourseID, CourseName FROM Courses";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        courses.Add(Convert.ToInt32(reader["CourseID"]), reader["CourseName"].ToString());
                    }
                }
            }

            return courses;
        }

        public async Task<Dictionary<int, string>> GetSubjectsAsync()
        {
            var subjects = new Dictionary<int, string>();

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT SubjectID, SubjectName FROM Subjects";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        subjects.Add(Convert.ToInt32(reader["SubjectID"]), reader["SubjectName"].ToString());
                    }
                }
            }

            return subjects;
        }
    }
}
 
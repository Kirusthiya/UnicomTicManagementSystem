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
    public class StudentSubjectConteroller
    {
            public async Task<bool> AddStudentSubjectAsync(string userId, int subjectId)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"INSERT INTO StudentSubjects (UserID, SubjectID)
                                     VALUES (@UserID, @SubjectID)";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.Parameters.AddWithValue("@SubjectID", subjectId);

                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding student subject: " + ex.Message);
                    return false;
                }
            }

            public async Task<bool> DeleteStudentSubjectAsync(int id)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "DELETE FROM StudentSubjects WHERE Id = @Id";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting student subject: " + ex.Message);
                    return false;
                }
            }

            public async Task<List<StudentSubject>> GetAllStudentSubjectsAsync()
            {
                var list = new List<StudentSubject>();
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"
                    SELECT ss.Id, ss.UserID, u.Name AS StudentName,
                           ss.SubjectID, s.SubjectName
                    FROM StudentSubjects ss
                    JOIN Users u ON ss.UserID = u.UserID
                    JOIN Subjects s ON ss.SubjectID = s.SubjectID";

                        using (var cmd = new SQLiteCommand(query, conn))
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new StudentSubject
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    UserID = reader["UserID"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                    SubjectName = reader["SubjectName"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching student subjects: " + ex.Message);
                }

                return list;
            }

            public async Task<List<StudentSubject>> SearchByStudentNameAsync(string name)
            {
                var list = new List<StudentSubject>();
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"
                    SELECT ss.Id, ss.UserID, u.Name AS StudentName,
                           ss.SubjectID, s.SubjectName
                    FROM StudentSubjects ss
                    JOIN Users u ON ss.UserID = u.UserID
                    JOIN Subjects s ON ss.SubjectID = s.SubjectID
                    WHERE u.Name LIKE @Name";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", $"%{name}%");

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    list.Add(new StudentSubject
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        UserID = reader["UserID"].ToString(),
                                        StudentName = reader["StudentName"].ToString(),
                                        SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                        SubjectName = reader["SubjectName"].ToString()
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error searching student subjects: " + ex.Message);
                }

                return list;
            }
        
    }
}
    



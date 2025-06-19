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
        public async Task<bool> AddStudentSubjectAsync(StudentSubject studentSubject)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"INSERT INTO StudentSubjects (UserID, SubjectID)
                                     VALUES (@UserID, @SubjectID)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", studentSubject.UserID);
                        cmd.Parameters.AddWithValue("@SubjectID", studentSubject.SubjectID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Error: " + ex.Message);
                return false;
            }
        }

        // Update
        public async Task<bool> UpdateStudentSubjectAsync(StudentSubject studentSubject)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"UPDATE StudentSubjects 
                                     SET UserID = @UserID, SubjectID = @SubjectID 
                                     WHERE ID = @ID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", studentSubject.UserID);
                        cmd.Parameters.AddWithValue("@SubjectID", studentSubject.SubjectID);
                        cmd.Parameters.AddWithValue("@ID", studentSubject.ID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error: " + ex.Message);
                return false;
            }
        }

        // Delete
        public async Task<bool> DeleteStudentSubjectAsync(int id)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "DELETE FROM StudentSubjects WHERE ID = @ID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Error: " + ex.Message);
                return false;
            }
        }

        // Get All with StudentName and SubjectName
        public async Task<List<StudentSubject>> GetAllStudentSubjectsAsync()
        {
            var list = new List<StudentSubject>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT ss.ID, ss.UserID, u.Name AS StudentName, 
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
                                ID = Convert.ToInt32(reader["ID"]),
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
                Console.WriteLine("GetAll Error: " + ex.Message);
            }

            return list;
        }

        // Search by Student Name
        public async Task<List<StudentSubject>> SearchByStudentNameAsync(string name)
        {
            var list = new List<StudentSubject>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT ss.ID, ss.UserID, u.Name AS StudentName, 
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
                                    ID = Convert.ToInt32(reader["ID"]),
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
                Console.WriteLine("Search Error: " + ex.Message);
            }

            return list;
        }
    }
}




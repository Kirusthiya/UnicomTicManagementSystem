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
    public class LecturerSubjectController
    {
        public async Task<bool> AddLecturerSubjectAsync(string userId, int subjectId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"INSERT INTO LecturerSubjects (UserID, SubjectID)
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
                Console.WriteLine("Error adding lecturer subject: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteLecturerSubjectAsync(int lecturerSubjectId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "DELETE FROM LecturerSubjects WHERE LecturerSubjectID = @ID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", lecturerSubjectId);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting lecturer subject: " + ex.Message);
                return false;
            }
        }

        public async Task<List<LecturerSubject>> GetAllLecturerSubjectsAsync()
        {
            var list = new List<LecturerSubject>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                    SELECT ls.LecturerSubjectID, ls.UserID, u.Name AS LecturerName, 
                           ls.SubjectID, s.SubjectName
                    FROM LecturerSubjects ls
                    JOIN Users u ON ls.UserID = u.UserID
                    JOIN Subjects s ON ls.SubjectID = s.SubjectID";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new LecturerSubject
                            {
                                LecturerSubjectID = Convert.ToInt32(reader["LecturerSubjectID"]),
                                UserID = reader["UserID"].ToString(),
                                SubjectID = Convert.ToInt32(reader["SubjectID"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving lecturer subjects: " + ex.Message);
            }

            return list;
        }

        public async Task<List<LecturerSubject>> SearchByLecturerNameAsync(string name)
        {
            var list = new List<LecturerSubject>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                    SELECT ls.LecturerSubjectID, ls.UserID, u.Name AS LecturerName, 
                           ls.SubjectID, s.SubjectName
                    FROM LecturerSubjects ls
                    JOIN Users u ON ls.UserID = u.UserID
                    JOIN Subjects s ON ls.SubjectID = s.SubjectID
                    WHERE u.Name LIKE @Name";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", $"%{name}%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new LecturerSubject
                                {
                                    LecturerSubjectID = Convert.ToInt32(reader["LecturerSubjectID"]),
                                    UserID = reader["UserID"].ToString(),
                                    SubjectID = Convert.ToInt32(reader["SubjectID"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching lecturer subjects: " + ex.Message);
            }

            return list;
        }
    }
}

        
    





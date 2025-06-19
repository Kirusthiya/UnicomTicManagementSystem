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
        public async Task<bool> AddLecturerSubjectAsync(LecturerSubject lecturerSubject)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    // Check for duplicate before insert
                    string checkQuery = @"SELECT COUNT(*) FROM LecturerSubjects 
                                          WHERE UserID = @UserID AND SubjectID = @SubjectID";
                    using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@UserID", lecturerSubject.UserID);
                        checkCmd.Parameters.AddWithValue("@SubjectID", lecturerSubject.SubjectID);

                        long count = (long)await checkCmd.ExecuteScalarAsync();
                        if (count > 0)
                        {
                            // Already exists
                            return false;
                        }
                    }

                    string query = @"INSERT INTO LecturerSubjects (UserID, SubjectID)
                                     VALUES (@UserID, @SubjectID)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", lecturerSubject.UserID);
                        cmd.Parameters.AddWithValue("@SubjectID", lecturerSubject.SubjectID);
                        int result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding LecturerSubject: " + ex.Message);
                return false;
            }
        }

        // Update LecturerSubject
        public async Task<bool> UpdateLecturerSubjectAsync(LecturerSubject lecturerSubject)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"UPDATE LecturerSubjects 
                                     SET UserID = @UserID, SubjectID = @SubjectID
                                     WHERE ID = @ID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", lecturerSubject.UserID);
                        cmd.Parameters.AddWithValue("@SubjectID", lecturerSubject.SubjectID);
                        cmd.Parameters.AddWithValue("@ID", lecturerSubject.ID);
                        int result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating LecturerSubject: " + ex.Message);
                return false;
            }
        }

        // Delete LecturerSubject by ID
        public async Task<bool> DeleteLecturerSubjectAsync(int id)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "DELETE FROM LecturerSubjects WHERE ID = @ID";
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
                Console.WriteLine("Error deleting LecturerSubject: " + ex.Message);
                return false;
            }
        }

        // Get all LecturerSubjects with Lecturer name and Subject name
        public async Task<List<LecturerSubject>> GetAllLecturerSubjectsAsync()
        {
            var list = new List<LecturerSubject>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT ls.ID, ls.UserID, u.Name AS LecturerName,
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
                                ID = Convert.ToInt32(reader["ID"]),
                                UserID = reader["UserID"].ToString(),
                                LecturerName = reader["LecturerName"].ToString(),
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                SubjectName = reader["SubjectName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching LecturerSubjects: " + ex.Message);
            }

            return list;
        }

        // Search by Lecturer Name
        public async Task<List<LecturerSubject>> SearchByLecturerNameAsync(string name)
        {
            var list = new List<LecturerSubject>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                        SELECT ls.ID, ls.UserID, u.Name AS LecturerName,
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
                                    ID = Convert.ToInt32(reader["ID"]),
                                    UserID = reader["UserID"].ToString(),
                                    LecturerName = reader["LecturerName"].ToString(),
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
                Console.WriteLine("Error searching LecturerSubjects: " + ex.Message);
            }

            return list;
        }
    }
}
 
    







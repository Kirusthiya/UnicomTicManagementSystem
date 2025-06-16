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
    public class TimeTableController
    {
            public async Task<bool> AddTimetableAsync(Timetable timetable)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"INSERT INTO Timetables (SubjectID, RoomID, TimeSlot, Day, UserID)
                                     VALUES (@SubjectID, @RoomID, @TimeSlot, @Day, @UserID)";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                            cmd.Parameters.AddWithValue("@RoomID", timetable.RoomID);
                            cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                            cmd.Parameters.AddWithValue("@Day", timetable.Day);
                            cmd.Parameters.AddWithValue("@UserID", timetable.UserID);

                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding timetable: " + ex.Message);
                    return false;
                }
            }

            public async Task<bool> UpdateTimetableAsync(Timetable timetable)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"UPDATE Timetables 
                                     SET SubjectID = @SubjectID, RoomID = @RoomID, TimeSlot = @TimeSlot, 
                                         Day = @Day, UserID = @UserID
                                     WHERE TimetableID = @TimetableID";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                            cmd.Parameters.AddWithValue("@RoomID", timetable.RoomID);
                            cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                            cmd.Parameters.AddWithValue("@Day", timetable.Day);
                            cmd.Parameters.AddWithValue("@UserID", timetable.UserID);
                            cmd.Parameters.AddWithValue("@TimetableID", timetable.TimetableID);

                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating timetable: " + ex.Message);
                    return false;
                }
            }

            public async Task<bool> DeleteTimetableAsync(int timetableId)
            {
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = "DELETE FROM Timetables WHERE TimetableID = @TimetableID";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@TimetableID", timetableId);
                            int rows = await cmd.ExecuteNonQueryAsync();
                            return rows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting timetable: " + ex.Message);
                    return false;
                }
            }

            public async Task<List<Timetable>> GetAllTimetablesAsync()
            {
                var timetables = new List<Timetable>();
                try
                {
                    using (var conn = DatabaseManager.GetConnection())
                    {
                        string query = @"
                        SELECT t.TimetableID, t.SubjectID, s.SubjectName, t.RoomID, r.RoomName, 
                               t.TimeSlot, t.Day, t.UserID, u.Name AS LecturerName
                        FROM Timetables t
                        JOIN Subjects s ON t.SubjectID = s.SubjectID
                        JOIN Rooms r ON t.RoomID = r.RoomID
                        JOIN Users u ON t.UserID = u.UserID";

                        using (var cmd = new SQLiteCommand(query, conn))
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                timetables.Add(new Timetable
                                {
                                    TimetableID = Convert.ToInt32(reader["TimetableID"]),
                                    SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                    SubjectName = reader["SubjectName"].ToString(),
                                    RoomID = Convert.ToInt32(reader["RoomID"]),
                                    RoomName = reader["RoomName"].ToString(),
                                    TimeSlot = reader["TimeSlot"].ToString(),
                                    Day = reader["Day"].ToString(),
                                    UserID = reader["UserID"].ToString(),
                                    LecturerName = reader["LecturerName"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading timetables: " + ex.Message);
                }

                return timetables;
            }
     
        public async Task<List<Timetable>> GetTimetablesByUserIDAsync(string userId)
        {
            var list = new List<Timetable>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = @"
                SELECT t.TimetableID, t.SubjectID, s.SubjectName, 
                       t.RoomID, r.RoomName, t.TimeSlot, t.Day, 
                       t.UserID, u.Name AS LecturerName
                       FROM Timetables t
                       JOIN Subjects s ON t.SubjectID = s.SubjectID
                       JOIN Rooms r ON t.RoomID = r.RoomID
                       JOIN Users u ON t.UserID = u.UserID
                       WHERE t.UserID = @UserID
                    ";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new Timetable
                                {
                                    TimetableID = Convert.ToInt32(reader["TimetableID"]),
                                    SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                    SubjectName = reader["SubjectName"].ToString(),
                                    RoomID = Convert.ToInt32(reader["RoomID"]),
                                    RoomName = reader["RoomName"].ToString(),
                                    TimeSlot = reader["TimeSlot"].ToString(),
                                    Day = reader["Day"].ToString(),
                                    UserID = reader["UserID"].ToString(),
                                    LecturerName = reader["LecturerName"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return list;
        }

    }

}
    

    



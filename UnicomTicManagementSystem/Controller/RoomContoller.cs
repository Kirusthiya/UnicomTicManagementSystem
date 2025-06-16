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
    public class RoomContoller
    {
        public async Task<bool> AddRoomAsync(Room room)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "INSERT INTO Rooms (RoomName, RoomType) VALUES (@RoomName, @RoomType)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomName", room.RoomName);
                        cmd.Parameters.AddWithValue("@RoomType", room.RoomType);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding room: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateRoomAsync(Room room)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "UPDATE Rooms SET RoomName = @RoomName, RoomType = @RoomType WHERE RoomID = @RoomID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomName", room.RoomName);
                        cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                        cmd.Parameters.AddWithValue("@RoomID", room.RoomID);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating room: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "DELETE FROM Rooms WHERE RoomID = @RoomID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomID", roomId);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting room: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            var rooms = new List<Room>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Rooms";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rooms.Add(new Room
                            {
                                RoomID = Convert.ToInt32(reader["RoomID"]),
                                RoomName = reader["RoomName"].ToString(),
                                RoomType = reader["RoomType"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching rooms: " + ex.Message);
            }

            return rooms;
        }

        public async Task<List<Room>> SearchRoomsAsync(string search)
        {
            var rooms = new List<Room>();
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    string query = "SELECT * FROM Rooms WHERE RoomName LIKE @Search OR RoomType LIKE @Search";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", $"%{search}%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                rooms.Add(new Room
                                {
                                    RoomID = Convert.ToInt32(reader["RoomID"]),
                                    RoomName = reader["RoomName"].ToString(),
                                    RoomType = reader["RoomType"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching rooms: " + ex.Message);
            }

            return rooms;
        }
    }
}
        







    



using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagementSystem.Repositories
{
    public static class DatabaseManager
    {
        public static string ConnectionString = "Data Source =UMCdatabaseDb.db; version=3;";

        public static SQLiteConnection GetConnection()
        {
            SQLiteConnection conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}

using System.Configuration;
using MySql.Data.MySqlClient;

namespace EazzyTravels
{
    // Reads the connection string named "EazzyTravelsDb" from Web.config
    // and hands out a ready-to-open MySqlConnection.
    public static class DbHelper
    {
        public static MySqlConnection GetConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["EazzyTravelsDb"].ConnectionString;
            return new MySqlConnection(connStr);
        }
    }
}

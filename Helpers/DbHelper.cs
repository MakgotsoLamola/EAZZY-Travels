using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace eeee.Helpers
{
    public static class DbHelper
    {
        private static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings["EaZZyTravelsDB"].ConnectionString; }
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnString);
        }
    }
}
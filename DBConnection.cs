using System;
using System.Configuration;
using MySql.Data.MySqlClient;

/// <summary>
/// Centralised MySQL connection helper for the EaZZy-Travels system.
/// Requires the MySql.Data NuGet package (MySql.Data.MySqlClient).
/// Connection string is read from Web.config:
///
/// <connectionStrings>
///   <add name="EaZZyTravelsDB"
///        connectionString="Server=localhost;Database=EaZZyTravelsDB;Uid=root;Pwd=yourpassword;"
///        providerName="MySql.Data.MySqlClient" />
/// </connectionStrings>
/// </summary>
public static class DBConnection
{
    private static readonly string ConnString =
        ConfigurationManager.ConnectionStrings["EaZZyTravelsDB"].ConnectionString;

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnString);
    }
}

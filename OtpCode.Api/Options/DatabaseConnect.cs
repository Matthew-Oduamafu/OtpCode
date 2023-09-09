using System.Data;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace OtpCode.Api.Options;

public class DatabaseConnect
{
    private IDbConnection Db { get; }

    public DatabaseConnect(IOptions<DatabaseConfig> databaseConfig)
    {
        Db = new MySqlConnection(databaseConfig.Value.DbConnectionString);
    }

    public IDbConnection CreateConnection()
    {
        return Db;
    }

    public void OpenConnection()
    {
        if (Db.State == ConnectionState.Closed) Db.Open();
    }

    public void CloseConnection()
    {
        if (Db.State == ConnectionState.Open) Db.Close();
    }
}
using Configuration;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Data;

public class DatabaseConnectionFactory(IOptions<DatabaseSettings> options) : IDatabaseConnectionFactory
{
    private readonly DatabaseSettings _settings = options.Value;

    public NpgsqlConnection CreateConnection()
    {
        string connectionString = _settings.GetConnectionString();
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
using Npgsql;

namespace Data;

public interface IDatabaseConnectionFactory
{
    NpgsqlConnection CreateConnection();
}
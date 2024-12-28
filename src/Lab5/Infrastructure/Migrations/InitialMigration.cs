using Npgsql;

namespace Migrations;

public class InitialMigration : BaseMigration
{
    public override string Id => "202310260001_InitialMigration";

    public override string Description => "Создание начальной схемы базы данных.";

    public override void Up(NpgsqlConnection connection)
    {
        string upSql = @"
                CREATE TABLE IF NOT EXISTS accounts (
                    account_number VARCHAR(10) PRIMARY KEY,
                    pin VARCHAR(4) NOT NULL,
                    balance DECIMAL NOT NULL DEFAULT 0
                );

                CREATE TABLE IF NOT EXISTS transactions (
                    id UUID PRIMARY KEY,
                    account_number VARCHAR(10) REFERENCES accounts(account_number),
                    amount DECIMAL NOT NULL,
                    timestamp TIMESTAMP NOT NULL DEFAULT NOW(),
                    type VARCHAR(20) NOT NULL
                );

                CREATE TABLE IF NOT EXISTS migrations (
                    id VARCHAR(50) PRIMARY KEY,
                    applied_at TIMESTAMP NOT NULL DEFAULT NOW(),
                    description TEXT
                );
            ";

        using var cmd = new NpgsqlCommand(upSql, connection);
        cmd.ExecuteNonQuery();
    }

    public override void Down(NpgsqlConnection connection)
    {
        string downSql = @"
                DROP TABLE IF EXISTS transactions;
                DROP TABLE IF EXISTS accounts;
                DROP TABLE IF EXISTS migrations;
            ";

        using var cmd = new NpgsqlCommand(downSql, connection);
        cmd.ExecuteNonQuery();
    }
}
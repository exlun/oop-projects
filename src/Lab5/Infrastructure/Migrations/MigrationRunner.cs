using Data;
using Npgsql;

namespace Migrations;

public class MigrationRunner(IDatabaseConnectionFactory connectionFactory, IEnumerable<BaseMigration> migrations)
{
    private readonly IEnumerable<BaseMigration> _migrations = migrations.OrderBy(m => m.Id);

    public void Migrate()
    {
        using NpgsqlConnection connection = connectionFactory.CreateConnection();
        string ensureMigrationsTable = @"
                CREATE TABLE IF NOT EXISTS migrations (
                    id VARCHAR(50) PRIMARY KEY,
                    applied_at TIMESTAMP NOT NULL DEFAULT NOW(),
                    description TEXT
                );
            ";

        using (var cmd = new NpgsqlCommand(ensureMigrationsTable, connection))
        {
            cmd.ExecuteNonQuery();
        }

        var appliedMigrations = new HashSet<string>();
        string selectMigrations = "SELECT id FROM migrations;";
        using (var cmd = new NpgsqlCommand(selectMigrations, connection))
        using (NpgsqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                appliedMigrations.Add(reader.GetString(0));
            }
        }

        foreach (BaseMigration migration in _migrations)
        {
            if (!appliedMigrations.Contains(migration.Id))
            {
                Console.WriteLine($"Применение миграции: {migration.Id} - {migration.Description}");
                migration.Up(connection);

                string insertMigration = @"
                        INSERT INTO migrations (id, description)
                        VALUES (@Id, @Description);
                    ";
                using var cmd = new NpgsqlCommand(insertMigration, connection);
                cmd.Parameters.AddWithValue("Id", migration.Id);
                cmd.Parameters.AddWithValue("Description", migration.Description);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Rollback(string migrationId)
    {
        using NpgsqlConnection connection = connectionFactory.CreateConnection();

        var migrationsToRollback = _migrations
            .Where(m => string.Compare(m.Id, migrationId, StringComparison.Ordinal) <= 0)
            .OrderByDescending(m => m.Id)
            .ToList();

        foreach (BaseMigration? migration in migrationsToRollback)
        {
            if (migration.Id == migrationId)
            {
                Console.WriteLine($"Откат миграции: {migration.Id} - {migration.Description}");
                migration.Down(connection);

                string deleteMigration = "DELETE FROM migrations WHERE id = @Id;";
                using var cmd = new NpgsqlCommand(deleteMigration, connection);
                cmd.Parameters.AddWithValue("Id", migration.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
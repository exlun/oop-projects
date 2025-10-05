using Npgsql;

namespace Migrations;

public abstract class BaseMigration
{
    public abstract string Id { get; }

    public abstract string Description { get; }

    public abstract void Up(NpgsqlConnection connection);

    public abstract void Down(NpgsqlConnection connection);
}
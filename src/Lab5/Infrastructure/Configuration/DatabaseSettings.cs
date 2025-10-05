namespace Configuration;

public class DatabaseSettings
{
    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 5432;

    public string Username { get; set; } = "postgres";

    public string Password { get; set; } = "postgres";

    public string Database { get; set; } = "atm_db";

    public string SslMode { get; set; } = "Prefer";

    public string GetConnectionString()
    {
        return
            $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};SSL Mode={SslMode};";
    }
}
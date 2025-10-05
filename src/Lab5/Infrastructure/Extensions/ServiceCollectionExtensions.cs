using Configuration;
using Data;
using Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migrations;
using Repositories;
using System.Reflection;

namespace Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        Type[] migrationTypes = Assembly.GetExecutingAssembly().GetTypes();
        var migrations = new List<BaseMigration>();

        migrations.Add(new InitialMigration());

        services.AddSingleton<IEnumerable<BaseMigration>>(migrations);

        services.AddTransient<MigrationRunner>();

        return services;
    }
}
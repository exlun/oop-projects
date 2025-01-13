using ApplicationExtensions;
using ApplicationInterfaces;
using ATM.ConsoleApp;
using DomainServices;
using Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrations;
using Services;
using UseCases;

namespace App;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Введите путь к конфигурационному файлу: ");
        string appsettingsPath = Console.ReadLine() ?? string.Empty;

        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(appsettingsPath, optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                IConfiguration configuration = context.Configuration;

                services
                    .AddApplication()
                    .AddInfrastructure(configuration);

                services.AddTransient<CreateAccountUseCase>();
                services.AddTransient<DepositMoneyUseCase>();
                services.AddTransient<WithdrawMoneyUseCase>();
                services.AddTransient<CheckBalanceUseCase>();
                services.AddTransient<GetTransactionHistoryUseCase>();

                services.AddTransient<Menu>();

                services.AddTransient<ITransactionService, TransactionService>();
                services.AddTransient<ATMService>();
            })
            .Build();

        using (IServiceScope scope = host.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            MigrationRunner migrationRunner = services.GetRequiredService<MigrationRunner>();
            migrationRunner.Migrate();
        }

        using (IServiceScope scope = host.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            Menu menu = services.GetRequiredService<Menu>();
            menu.Display();
        }
    }
}
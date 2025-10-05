using ApplicationInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace ApplicationExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
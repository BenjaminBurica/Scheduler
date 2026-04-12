using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.Replication;
namespace Data;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}

using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}



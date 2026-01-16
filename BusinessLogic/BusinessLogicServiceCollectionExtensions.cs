using System;
using BusinessLogic.UseCases;
using Data;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class BusinessLogicServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddDataServices();
        services.AddScoped<IGetUser, GetUser>();
        return services;
    }
}

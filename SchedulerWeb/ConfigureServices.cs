using System;
using BusinessLogic;
using Entities;
using Data;
using Data.Repositories;
using BusinessLogic.UseCases;

namespace SchedulerWeb;

public static class ConfigureServices
{
    public static IServiceCollection AddSchedulerServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGetUser, GetUser>();

        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<ISaveSchedule, SaveSchedule>();
        services.AddScoped<IGetSchedule, GetSchedule>();

        services.AddScoped<IServiceProviderRepository, ServiceProviderRepository>();
        services.AddScoped<IGetServiceProvider, GetServiceProvider>();

        return services;
    }
}

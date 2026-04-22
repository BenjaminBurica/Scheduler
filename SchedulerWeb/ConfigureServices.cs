using System;
using BusinessLogic;

namespace SchedulerWeb;

public static class ConfigureServices
{
    public static IServiceCollection AddSchedulerServices(this IServiceCollection services)
    {
        services.AddBusinessLogicServices();
        return services;
    }
}

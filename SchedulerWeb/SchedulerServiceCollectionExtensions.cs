using BusinessLogic;
using BusinessLogic.UseCases;
namespace Scheduler;

public static class SchedulerServiceCollectionExtensions
{
    public static IServiceCollection AddSchedulerServices(this IServiceCollection services)
    {
        services.AddBusinessLogicServices();
        return services;
    }
}
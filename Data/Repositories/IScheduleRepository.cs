namespace Data.Repositories;
using Entities;
public interface IScheduleRepository
{
    Task SaveScheduleAsync(int id, List<StoreSchedule> schedule);
    Task<List<StoreSchedule>> GetScheduleAsync(int id);
}
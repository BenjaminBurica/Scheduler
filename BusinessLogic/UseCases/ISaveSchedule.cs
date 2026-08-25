namespace BusinessLogic.UseCases;
using Entities;

public interface ISaveSchedule
{
    Task SaveScheduleAsync(int id, List<StoreSchedule> schedule);

}
namespace BusinessLogic.UseCases;

using Entities;

public interface IGetSchedule
{
    Task<List<StoreSchedule>> GetAsync(int id);
}
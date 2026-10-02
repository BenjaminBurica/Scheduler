namespace BusinessLogic.UseCases;
using Entities;
using Data.Repositories;
public class SaveSchedule : ISaveSchedule
{
    private readonly IScheduleRepository _repository;
    public SaveSchedule(IScheduleRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<StoreSchedule>> GetAsync(int id)
    {
        return await _repository.GetScheduleAsync(id);
    }

    public Task SaveScheduleAsync(int id, List<StoreSchedule> schedule)
    {
        return _repository.SaveScheduleAsync(id, schedule);
    }
}
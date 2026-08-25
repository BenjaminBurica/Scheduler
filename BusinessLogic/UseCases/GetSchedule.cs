namespace BusinessLogic.UseCases;
using BusinessLogic.Models;
using Data.Repositories;
using Entities;
// get schedule
public class GetSchedule : IGetSchedule
{
    private readonly IScheduleRepository repository;
    public GetSchedule(IScheduleRepository repository)
    {
        this.repository = repository;
    }

    public Task<List<StoreSchedule>> GetAsync(int id)
    {
        return repository.GetScheduleAsync(id);
    }
}
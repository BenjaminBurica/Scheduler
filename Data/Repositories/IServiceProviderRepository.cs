namespace Data.Repositories;
using Entities;

public interface IServiceProviderRepository
{
    Task<ServiceProvider?> GetAsync(int id);
}
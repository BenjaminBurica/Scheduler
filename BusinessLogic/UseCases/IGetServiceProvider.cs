namespace BusinessLogic.UseCases;

using BusinessLogic.Models;
using Entities;

public interface IGetServiceProvider
{
    Task<ServiceProvider?> GetAsync(int id);
}

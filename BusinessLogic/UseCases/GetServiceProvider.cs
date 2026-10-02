namespace BusinessLogic.UseCases;
using BusinessLogic.Models;
using Data.Repositories;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Entities;

public class GetServiceProvider : IGetServiceProvider
{
    private readonly IServiceProviderRepository _repository;
    public GetServiceProvider(IServiceProviderRepository repository)
    {
        _repository = repository;
    }
    public Task<ServiceProvider?> GetAsync(int id)
    {
        return _repository.GetAsync(id);
    }
}
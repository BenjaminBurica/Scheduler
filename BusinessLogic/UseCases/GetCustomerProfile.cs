using Entities;
using Data.Repositories;

namespace BusinessLogic.UseCases;

public class GetCustomerProfile : IGetCustomerProfiles
{
    private readonly ICustomerProfileRepository repository;
    public GetCustomerProfile(ICustomerProfileRepository repository)
    {
        this.repository = repository;
    }

    public async Task<List<CustomerProfile>> GetAsync(int serviceProviderId)
    {
        return await repository.GetCustomerProfilesAsync(serviceProviderId);
    }
}
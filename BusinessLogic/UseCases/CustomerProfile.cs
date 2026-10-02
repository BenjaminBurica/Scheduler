using Entities;
using Data.Repositories;

namespace BusinessLogic.UseCases;
public class SaveCustomerProfile : ISaveCustomerProfile
{
    private readonly ICustomerProfileRepository repository;

    public SaveCustomerProfile(ICustomerProfileRepository repository)
    {
        this.repository = repository;
    }

    public async Task<int> SaveAsync(CustomerProfile customerProfile)
    {
        return await repository.SaveCustomerProfileAsync(customerProfile);
    }
}

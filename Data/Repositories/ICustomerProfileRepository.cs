using Entities;

namespace Data.Repositories;

public interface ICustomerProfileRepository
{
    Task<int> SaveCustomerProfileAsync(CustomerProfile customerProfile);

    Task<List<CustomerProfile>> GetCustomerProfilesAsync(
        int serviceProviderId);
}
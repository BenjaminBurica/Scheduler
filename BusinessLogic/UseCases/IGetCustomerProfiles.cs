using Entities;
namespace BusinessLogic.UseCases;

public interface IGetCustomerProfiles
{
    Task<List<CustomerProfile>> GetAsync(int serviceProviderId);
}
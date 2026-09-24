using Entities;

namespace BusinessLogic.UseCases;

public interface ISaveCustomerProfile
{
    Task<int> SaveAsync(CustomerProfile customerProfile);
}
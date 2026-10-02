namespace Entities;

public class CustomerProfile
{
    public int Id { get; set; }

    public int ServiceProviderId { get; set; }

    public string FirstName { get; set; } = "";

    public string ServiceType { get; set; } = "";

    public List<CustomerProfileStep> Steps { get; set; } = new();
}
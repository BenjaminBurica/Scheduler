using Microsoft.AspNetCore.Mvc;
using BusinessLogic.UseCases;
using Entities;
using Microsoft.AspNetCore.Authorization;

namespace SchedulerWeb.Controllers;

[Authorize(Roles = "Admin,ServiceProvider")]
[Route("api/customer-profiles")]
[ApiController]

public class CustomerProfileApiController : ControllerBase
{
    private readonly IGetCustomerProfiles getCustomerProfiles;
    private readonly ISaveCustomerProfile saveCustomerProfile;

    public CustomerProfileApiController(
        IGetCustomerProfiles getCustomerProfiles,
        ISaveCustomerProfile saveCustomerProfile)
    {
        this.getCustomerProfiles = getCustomerProfiles;
        this.saveCustomerProfile = saveCustomerProfile;
    }

    [HttpGet("{serviceProviderId}")]
    public async Task<ActionResult<List<CustomerProfile>>> Get(int serviceProviderId)
    {
        var profiles = await getCustomerProfiles.GetAsync(serviceProviderId);
        return Ok(profiles);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] CustomerProfile customerProfile)
    {
        var id = await saveCustomerProfile.SaveAsync(customerProfile);
        return Ok(new { customerProfileId = id });
    }

}

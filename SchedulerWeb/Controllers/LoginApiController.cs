using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using BusinessLogic.UseCases;
using SchedulerWeb.Models;
namespace SchedulerWeb.Controllers

{
    [Route("api/login")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly IGetUser getUser;

        public LoginApiController(IGetUser getUser)
        {
            this.getUser = getUser;
        }

        [HttpPost("login")]
        public async Task<LoginApiResponse> Login(LoginApiRequest request)
        {
            if (request.PhoneNumber != null) {
                var userRequest = new GetUserRequest {PhoneNumber = request.PhoneNumber};
                var response = await getUser.ExecuteAsync(userRequest);
                if (response.User != null)
                {
                    return new LoginApiResponse { Success = true };
                } else
                {
                    return new LoginApiResponse { Success = false };
                }
            } else
            {
                return new LoginApiResponse { Success = false };
            }
        }
        
    }
}

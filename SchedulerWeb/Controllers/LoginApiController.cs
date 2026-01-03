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
        [HttpPost("login")]
        public LoginApiResponse Login(LoginApiRequest request)
        {
            if (request.PhoneNumber != null) {
                var userRequest = new GetUserRequest {PhoneNumber = request.PhoneNumber};
                var getUser = new GetUser();
                var response = getUser.Execute(userRequest);
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

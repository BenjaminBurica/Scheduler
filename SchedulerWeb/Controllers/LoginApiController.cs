using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using BusinessLogic.UseCases;
using SchedulerWeb.Models;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;
using SchedulerWeb;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using Entities;
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
                var user = response.User;
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(CustomClaimTypes.UserId, user.UserId?.ToString(CultureInfo.InvariantCulture) ?? ""),
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}".Trim()),
                        new Claim(CustomClaimTypes.Role, user.Role?.ToString() ?? Role.Uknown.ToString()),
                        new Claim(CustomClaimTypes.FirstName, user.FirstName ?? ""),
                        new Claim(ClaimTypes.Surname, user.LastName ?? ""),
                        new Claim(CustomClaimTypes.PhoneNumber, user.PhoneNumber ?? "")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                }          
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

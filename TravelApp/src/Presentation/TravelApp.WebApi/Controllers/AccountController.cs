using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Application.Common.Interfaces;
using TravelApp.WebApi.Controllers;

namespace TravelApp.Presentation.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var (success, token) = await _identityService.LoginAsync(request.Email, request.Password);

            if (!success)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return new LoginResponse { Token = token, Email = request.Email };
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}

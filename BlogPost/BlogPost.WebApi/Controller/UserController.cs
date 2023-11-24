using BlogPost.WebApi.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.WebApi.Controller
{
    public class UserController : ControllerBase
    {
       private readonly IMockAuthenticationService _authenticationService;

        public UserController(IMockAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_authenticationService.AuthenticateUser(request.Username, request.Password))
            {
                var token = _authenticationService.GenerateToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


}

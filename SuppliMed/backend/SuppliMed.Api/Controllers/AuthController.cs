using Microsoft.AspNetCore.Mvc;
using AppCore.Services; 
using AppCore.Models;

namespace SuppliMed.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        Console.WriteLine($"Login attempt for: {request.Username}");

        var result = AuthService.Login(request.Username, request.Password);

        if (result.Status == AuthService.LoginResultStatus.Success)
        {
            HttpContext.Session.SetString("Role", result.User.GetRole());

            HttpContext.Session.SetString("Username", result.User.Username);
            return Ok(new { 
                message = "Welcome back!", 
                user = result.User?.Username, 
                role = result.User?.GetRole()
                });
        }
        
        if (result.Status == AuthService.LoginResultStatus.LockedOut)
        {
            return StatusCode(423, new { 
                message = "Account frozen due to too many attempts.", 
                secondsRemaining = result.TimeRemainingSeconds 
            });
        }

        return Unauthorized(new { message = "Invalid credentials" });
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

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
        // Calling your static AuthService directly
        var result = AuthService.Login(request.Username, request.Password);

        if (result.Status == AuthService.LoginResultStatus.Success)
        {
            return Ok(new { message = "Welcome back!", user = result.User?.Username });
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
}

using Microsoft.AspNetCore.Mvc;

namespace SuppliMed.Api.Controllers;

[ApiController]
[Route("api/logout")] // Hardcode it to lowercase here
public class LogoutController : ControllerBase
{
    [HttpPost]
    public IActionResult Logout()
    {
        return Ok(new { message = "Session ended successfully." });
    }
}
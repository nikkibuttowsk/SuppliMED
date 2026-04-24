using Microsoft.AspNetCore.Mvc;
using AppCore.Services;
using AppCore.Models;

namespace SuppliMed.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly InventoryServices _inventoryService;

    public InventoryController(InventoryServices inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet("dashboard-summary")]
    public IActionResult GetDashboardSummary()
    {
        var all = _inventoryService.GetAllSupplies();
        var low = _inventoryService.GetLowStockSupplies();
        var expired = _inventoryService.GetExpiredSupplies();
        // assumption of 30 days for "Expiring Soon"
        var expiring = _inventoryService.GetFilteredExpiringSupplies(30); 

        return Ok(new {
            totalCount = all.Count,
            lowStockCount = low.Count,
            expiredCount = expired.Count,
            lowStockItems = low.Select(s => new { s.Name, Brand = s is Medicine m ? m.Brand : "N/A", s.Quantity }),
            expiringItems = expiring.Select(s => new { s.Name, s.Id })
        });
    }
}
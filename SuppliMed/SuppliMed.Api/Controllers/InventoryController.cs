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

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        var supplies = _inventoryService.GetAllSupplies();
        var lowStock = _inventoryService.GetLowStockSupplies();
        var expired = _inventoryService.GetExpiredSupplies();
        
        return Ok(new {
            totalSupply = supplies.Count,
            lowStockCount = lowStock.Count,
            expiredCount = expired.Count
        });
    }
}
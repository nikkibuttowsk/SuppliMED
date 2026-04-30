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

    [HttpGet]
    public IActionResult GetAll()
    {
        var supplies = _inventoryService.GetAllSupplies()
            .Select(s => new {
                s.Id,
                s.Name,
                s.Brand,
                s.Quantity,
                expirationDate = s.GetDisplayExpiryDate(), 
                s.MinimumStock
            });
        return Ok(supplies);
    }

    [HttpGet("dashboard-summary")]
    public IActionResult GetDashboardSummary()
    {
        try 
        {
            var all = _inventoryService.GetAllSupplies() ?? new List<MedicalSupply>();
            
            var lowStockItems = _inventoryService.GetLowStockSupplies()
                .Select(s => new {
                    id = s.Id,
                    name = s.Name ?? "Unknown",
                    brand = s.Brand ?? (s is Equipment e ? e.SerialNumber : "N/A"),
                    quantity = s.Quantity,
                    minimumStock = s.MinimumStock,
                    // Avoid division by zero
                    statusPercentage = s.MinimumStock > 0 
                        ? Math.Round((double)s.Quantity / s.MinimumStock * 100, 1) 
                        : 0
                })
                .Take(5)
                .ToList();

            var expiring = _inventoryService.GetFilteredExpiringSupplies(30)
                .Select(s => new {
                    name = s.Name ?? "Unknown",
                    id = s.Id,
                    // Ensure this method handles null batches safely
                    expiryDate = s.GetDisplayExpiryDate() ?? "No Date" 
                })
                .ToList();

            return Ok(new {
                totalCount = all.Count,
                lowStockCount = _inventoryService.GetLowStockCount(),
                expiredCount = _inventoryService.GetExpiredCount(),
                lowStockItems = lowStockItems,
                expiringItems = expiring
            });
        }
        catch (Exception ex)
        {
            // This will print the ACTUAL error in your terminal so you can see it
            Console.WriteLine($"Dashboard Error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var supply = _inventoryService.GetSupplyById(id);
        if (supply == null) return NotFound();
        return Ok(supply);
    }

// 1. FOR NEW ENTRIES (Registration)
    [HttpPost("add")]
    public IActionResult AddNewSupply([FromBody] SupplyRequest request)
    {
        try 
        {
            MedicalSupply newSupply;
            if (request.Category == "Medicine")
            {
                newSupply = new Medicine {
                    Id = request.Id,
                    Name = request.Name,
                    Brand = request.Brand,
                    MinimumStock = request.MinimumStock,
                    Batches = new List<Batch> {
                        new Batch { 
                            MedicalSupplyId = request.Id,
                            BatchNumber = request.BatchNumber, 
                            Quantity = request.Quantity, 
                            ExpirationDate = request.ExpiryDate ?? DateTime.Now.AddMonths(6)
                        }
                    }
                };
            }
            else
            {
                newSupply = new Equipment {
                    Id = request.Id,
                    Name = request.Name,
                    Brand = request.Brand,
                    MinimumStock = request.MinimumStock,
                    Quantity = request.Quantity,
                    SerialNumber = request.SerialNumber
                };
            }

            _inventoryService.AddSupply(newSupply);
            return Ok(new { message = "Successfully registered new supply" });
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    // restocking existing supplies (for both medicines and equipment)
    [HttpPost("add-stock")]
    public IActionResult AddStock([FromBody] StockRequest request)
    {
        var existing = _inventoryService.GetSupplyById(request.Id);
        if (existing == null) return NotFound("Supply ID not found. Use 'Add' to register it first.");

        _inventoryService.AddStock(request.Id, request.Quantity, request.BatchNumber);
        return Ok(new { message = "Stock updated successfully" });
    }


    [HttpPost("remove-stock")]
    public IActionResult RemoveStock([FromBody] StockRequest request)
    {
        try {
            _inventoryService.RemoveStock(request.Id, request.Quantity);
            return Ok();
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
    //delete action
    [HttpDelete("{id}")]
    public IActionResult DeleteSupply(string id)
    {
        try
        {
            var adminUser = AuthService.GetAllUsers().OfType<Admin>().FirstOrDefault();

            if (adminUser == null) 
                return StatusCode(500, "System Error: No Admin account exists in AuthService.");
                
            _inventoryService.DeleteSupply(id, adminUser);
            return Ok(new { message = $"Item {id} deleted successfully." });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // --- UPDATE (RESTOCK/REDUCE) ACTION ---
    [HttpPost("update-stock")]
    public IActionResult UpdateStock([FromBody] UpdateStockRequest request)
    {
        var supply = _inventoryService.GetSupplyById(request.Id);
        if (supply == null) return NotFound("Supply ID not found.");

        try
        {
            _inventoryService.ProcessStockUpdate(request.Id, request.Quantity, request.BatchNumber);
            return Ok(new { status = "Success", message = "Inventory synchronized." });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }

    // DTO for the Update Request
    public class UpdateStockRequest
    {
        public required string Id { get; set; }
        public int Quantity { get; set; } // Positive for Add, Negative for Remove
        public string? BatchNumber { get; set; }
    }

    // Helper DTOs
    public class SupplyRequest 
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Brand { get; set; }
        public required string Category { get; set; }
        public int MinimumStock { get; set; }
        public int Quantity { get; set; }
        public string? SerialNumber { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class StockRequest 
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string BatchNumber { get; set; }
    }

    [HttpGet("transactions")]
    public IActionResult GetTransactions()
    {
        // Fetches the list of transactions from the service
        var transactions = _inventoryService.GetAllTransactions()
            .Select(t => new {
                logId = t.LogId,
                dateTime = t.DateTime.ToString("yyyy-MM-ddTHH:mm:ssZ"), // ISO Format for JS
                user = t.User,
                action = t.Action,
                item = t.Item,
                details = t.Details
            })
            .OrderByDescending(t => t.dateTime) // Newest logs first
            .ToList();

        return Ok(transactions);
    }

}
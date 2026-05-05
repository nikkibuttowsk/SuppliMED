namespace AppCore.Models
{
    public class AuditLog {
    public required string LogId { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public required string User { get; set; } = "System"; // Link this to the current logged-in user 
    public required string Action { get; set; } // ADD, UPDATE, DELETE, RESTOCK
    public required string Item { get; set; } // The name or ID of the supply
    public required string Details { get; set; }
}
}
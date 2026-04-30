namespace AppCore.Models
{
    public class Transaction {
    public int LogId { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string User { get; set; } = "System"; // Link this to the current logged-in user 
    public string Action { get; set; } // ADD, UPDATE, DELETE, RESTOCK
    public string Item { get; set; } // The name or ID of the supply
    public string Details { get; set; }
}
}
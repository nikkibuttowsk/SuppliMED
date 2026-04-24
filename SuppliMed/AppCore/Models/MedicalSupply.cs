using System;

namespace AppCore.Models
{
    public abstract class MedicalSupply
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        
        public string? Brand { get; set; } 
        
        public int Quantity { get; set; }
        public int MinimumStock { get; set; }

        public void AddStock(int amount)
        {
            if (amount < 0) throw new ArgumentException("Amount must be positive.");
            Quantity += amount;
        }

        public void ReduceStock(int amount)
        {
            if (amount < 0) throw new ArgumentException("Amount must be positive.");
            
            if (amount > Quantity)
                throw new InvalidOperationException("Insufficient stock to complete this transaction.");

            Quantity -= amount;
        }

        public bool IsLowStock()
        {
            return Quantity <= MinimumStock;
        }

        // This matches the "Status" column in your UI design
        public string GetStatusText() 
        {
            return IsLowStock() ? "Low Stock" : "Sufficient Stock";
        }

        public virtual string GetDetails()
        {
            return $"{Id} - {Name} ({Brand}) | Qty: {Quantity} | Status: {GetStatusText()}";
        }

        public string GetDisplayExpiryDate()
        {
            // medicine check
            if (this is Medicine med && med.Batches != null && med.Batches.Any())
            {
                // Returns the date of the batch expiring the soonest
                return med.Batches.Min(b => b.ExpirationDate).ToString("yyyy-MM-dd");
            }
            
            // for non-medicine supplies
            return "-"; 
        }
    }
}
using System;

namespace AppCore.Models
{
    public abstract class MedicalSupply
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
        
        public string? Brand { get; set; } 
        
        public virtual int Quantity { get; set; }
        public int MinimumStock { get; set; }
        public string Category { get; set;} = string.Empty;
        public ICollection<Batch> Batches { get; set; } = new List<Batch>();

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

        public virtual string GetDisplayExpiryDate() 
        {
            // no expiry for equipment or if batches are missing
            if (this is not Medicine med || med.Batches == null || !med.Batches.Any()) 
            {
                return "N/A";
            }

            // For Medicine, find the batch that expires SOONEST
            var soonest = med.Batches.OrderBy(b => b.ExpirationDate).First();
            return soonest.ExpirationDate.ToString("MMM dd, yyyy");
        }
    }
}
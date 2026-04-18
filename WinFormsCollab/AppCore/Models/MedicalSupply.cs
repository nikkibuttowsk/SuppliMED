using System;

namespace AppCore.Models
{
    public abstract class MedicalSupply
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string Brand { get; set; } 
        
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }

        public void AddStock(int amount)
        {
            if (amount < 0) throw new ArgumentException("Amount must be positive.");
            CurrentStock += amount;
        }

        public void ReduceStock(int amount)
        {
            if (amount < 0) throw new ArgumentException("Amount must be positive.");
            
            if (amount > CurrentStock)
                throw new InvalidOperationException("Insufficient stock to complete this transaction.");

            CurrentStock -= amount;
        }

        public bool IsLowStock()
        {
            return CurrentStock <= MinimumStock;
        }

        // This matches the "Status" column in your UI design
        public string GetStatusText() 
        {
            return IsLowStock() ? "Low Stock" : "Sufficient Stock";
        }

        public virtual string GetDetails()
        {
            return $"{Id} - {Name} ({Brand}) | Qty: {CurrentStock}";
        }
    }
}
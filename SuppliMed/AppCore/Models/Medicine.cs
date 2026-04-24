using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore.Models
{
    public class Medicine : MedicalSupply
    {
        public List<Batch> Batches { get; set; } = new List<Batch>();

        // Total stock is the sum of all batch quantities
        public new int Quantity => Batches.Sum(b => b.Quantity);

        // This replaces the old static ExpirationDate property
        // It dynamically finds the earliest expiration date among active batches
        public DateTime? EarliestExpirationDate => 
            Batches.Where(b => b.Quantity > 0)
                .OrderBy(b => b.ExpirationDate)
                .Select(b => b.ExpirationDate)
                .Cast<DateTime?>()
                .FirstOrDefault();

        public bool IsAnyBatchExpired() => Batches.Any(b => b.IsExpired());
        
        // Checks if ANY batch is expiring soon (based on your dashboard threshold)
        public bool IsExpiringSoon(int days)
        {
            var nextDate = GetNextExpirationDate();
            if (nextDate == null) return false;

            return (nextDate.Value - DateTime.Now).TotalDays <= days;
        }

        // Returns the date for the batch that will expire next
        public DateTime? GetNextExpirationDate() => 
            Batches.Where(b => !b.IsExpired() && b.Quantity > 0)
                .OrderBy(b => b.ExpirationDate)
                .Select(b => b.ExpirationDate)
                .Cast<DateTime?>()
                .FirstOrDefault();

        public override string GetDetails()
        {
            string expiryInfo = EarliestExpirationDate.HasValue 
                ? EarliestExpirationDate.Value.ToString("yyyy-MM-dd") 
                : "No Active Batches";

            return base.GetDetails() + $" | Next Expiry: {expiryInfo} | Batches: {Batches.Count}";
        }
    }
}
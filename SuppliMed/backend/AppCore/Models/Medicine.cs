using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore.Models
{
    public class Medicine : MedicalSupply
    {
        // Total stock is the sum of all batch quantities
        public override int Quantity 
        { 
            get => Batches?.Sum(b => b.Quantity) ?? 0;
            set {}
        }
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
            var today = DateTime.Today;

            return Batches.Any(b =>
                b.Quantity > 0 &&
                !b.IsExpired() &&
                b.ExpirationDate.Date <= today.AddDays(days)
            );
        }

        // Returns the date for the batch that will expire next
        public DateTime? GetNextExpirationDate() => 
            Batches.Where(b => b.Quantity > 0)
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
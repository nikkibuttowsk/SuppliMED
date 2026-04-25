using System;

namespace AppCore.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public required string BatchNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        public required string MedicalSupplyId { get; set; } // The ID of the parent supply
        public MedicalSupply? MedicalSupply { get; set; }
        public bool IsExpired()
        {
            return DateTime.Now.Date >= ExpirationDate.Date;
        }

        public bool IsExpiringSoon(int days)
        {
            double remainingDays = (ExpirationDate.Date - DateTime.Now.Date).TotalDays;
            return remainingDays >= 0 && remainingDays <= days;
        }
    }
}
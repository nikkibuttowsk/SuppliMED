using System;

namespace AppCore.Models
{
    public class Batch
    {
        public string BatchNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        
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
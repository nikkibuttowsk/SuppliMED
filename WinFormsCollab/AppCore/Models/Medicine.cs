namespace AppCore.Models
{
    public class Medicine : MedicalSupply
    {
        public DateTime ExpirationDate { get; set; }

        public bool IsExpired()
        {
            return DateTime.Now > ExpirationDate;
        }

        public bool IsExpiringSoon(int days)
        {
            return (ExpirationDate - DateTime.Now).TotalDays <= days;
        }

        public override string GetDetails()
        {
            return base.GetDetails() + $" | Expires: {ExpirationDate}";
        }
    }
}
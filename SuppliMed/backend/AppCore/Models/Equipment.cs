namespace AppCore.Models
{
    public class Equipment : MedicalSupply
    {
        public string? SerialNumber { get; set; }

        public override string GetDetails()
        {
            return base.GetDetails() + $" | Serial: {SerialNumber}";
        }
    }
}

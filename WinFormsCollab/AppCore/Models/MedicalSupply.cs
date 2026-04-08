namespace AppCore.Models
{
    public class MedicalSupply
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Medicine : MedicalSupply
{
    public DateTime ExpirationDate { get; set; }
}

    public class Equipment : MedicalSupply
    {
        public string SerialNumber { get; set; }
    }
}
namespace AppCore.Models
{
    public class Equipment : MedicalSupply
    {
        public string SerialNumber { get; set; }
        public DateTime LastMaintenanceDate { get; set; }

        public bool NeedsMaintenance(int months)
        {
            return (DateTime.Now - LastMaintenanceDate).TotalDays >= months * 30;
        }

        public void PerformMaintenance()
        {
            LastMaintenanceDate = DateTime.Now;
        }

        public override string GetDetails()
        {
            return base.GetDetails() + $" | Serial: {SerialNumber}";
        }
    }
}

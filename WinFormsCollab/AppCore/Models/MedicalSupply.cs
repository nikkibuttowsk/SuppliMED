namespace AppCore.Models
{
    public abstract class MedicalSupply
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }

        public void AddStock(int amount)
        {
            CurrentStock += amount;
        }

        public void ReduceStock(int amount)
        {
            if (amount > CurrentStock)
                throw new Exception("Not enough stock");

            CurrentStock -= amount;
        }

        public bool IsLowStock()
        {
            return CurrentStock <= MinimumStock;
        }

        public virtual string GetDetails()
        {
            return $"{Id} - {Name} | Qty: {CurrentStock}";
        }
    }
}
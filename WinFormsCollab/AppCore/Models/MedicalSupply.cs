namespace AppCore.Models
{
    public class MedicalSupply
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int MinimumStock { get; set; }

        public void AddStock(int amount)
        {
            Quantity += amount;
        }

        public void ReduceStock(int amount)
        {
            if (amount > Quantity)
                throw new Exception("Not enough stock");

            Quantity -= amount;
        }

        public bool IsLowStock()
        {
            return Quantity <= MinimumStock;
        }

        public virtual string GetDetails()
        {
            return $"{Id} - {Name} | Qty: {Quantity}";
        }
    }
}
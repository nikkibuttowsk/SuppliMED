using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services
{
    public class InventoryServices : IInventoryService
    {
        private List<MedicalSupply> supplies = new();
        private List<Transaction> transactions = new();

        public void AddSupply(MedicalSupply supply)
        {
            supplies.Add(supply);
        }

        public List<MedicalSupply> GetAllSupplies()
        {
            return supplies;
        }

        public MedicalSupply GetSupplyById(string id)
        {
            return supplies.FirstOrDefault(s => s.Id == id);
        }

        public void UpdateSupply(MedicalSupply supply)
        {
            var existing = GetSupplyById(supply.Id);
            if (existing != null)
            {
                existing.Name = supply.Name;
                existing.Quantity = supply.Quantity;
                existing.MinimumStock = supply.MinimumStock;
            }
        }

        public void DeleteSupply(string id)
        {
            var supply = GetSupplyById(id);
            if (supply != null)
            {
                supplies.Remove(supply);
            }
        }

        public void AddStock(string id, int qty)
        {
            var supply = GetSupplyById(id);
            if (supply != null)
            {
                supply.AddStock(qty);
                RecordTransaction(id, qty, "ADD");
            }
        }

        public void RemoveStock(string id, int qty)
        {
            var supply = GetSupplyById(id);
            if (supply != null)
            {
                supply.ReduceStock(qty);
                RecordTransaction(id, qty, "REMOVE");
            }
        }

        public List<MedicalSupply> GetLowStockSupplies()
        {
            return supplies.Where(s => s.IsLowStock()).ToList();
        }

        public List<MedicalSupply> GetExpiringSupplies(int days)
        {
            return supplies
                .OfType<Medicine>()
                .Where(m => m.IsExpiringSoon(days))
                .Cast<MedicalSupply>()
                .ToList();
        }

        private void RecordTransaction(string id, int qty, string type)
        {
            transactions.Add(new Transaction
            {
                TransactionID = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                Quantity = qty,
                Type = type
            });
        }

        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }

        public void DeleteSupply(string id, User user)
        {
            if (user is not Admin)
                throw new Exception("Access denied");

            var supply = GetSupplyById(id);
            if (supply != null)
                supplies.Remove(supply);
        }
    }
}
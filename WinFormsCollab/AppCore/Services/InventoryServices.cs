using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services
{
    public class InventoryService : IInventoryService
    {
        private List<MedicalSupply> _supplies = new List<MedicalSupply>();

        public void AddSupply(MedicalSupply supply)
        {
            _supplies.Add(supply);
        }

        public List<MedicalSupply> GetAllSupplies()
        {
            return _supplies;
        }

        public MedicalSupply GetSupplyById(string id)
        {
            return _supplies.FirstOrDefault(s => s.Id == id);
        }

        public void UpdateSupply(MedicalSupply updatedSupply)
        {
            var supply = GetSupplyById(updatedSupply.Id);
            if (supply != null)
            {
                supply.Name = updatedSupply.Name;
                supply.Category = updatedSupply.Category;
                supply.Quantity = updatedSupply.Quantity;
                supply.MinimumStock = updatedSupply.MinimumStock;
                supply.ExpirationDate = updatedSupply.ExpirationDate;
            }
        }

        public void DeleteSupply(string id)
        {
            var supply = GetSupplyById(id);
            if (supply != null)
            {
                _supplies.Remove(supply);
            }
        }

        public List<MedicalSupply> GetLowStockSupplies()
        {
            return _supplies.Where(s => s.Quantity <= s.MinimumStock).ToList();
        }

        public List<MedicalSupply> GetExpiringSupplies(int days)
        {
            var targetDate = DateTime.Now.AddDays(days);
            return _supplies
                .Where(s => s.ExpirationDate <= targetDate)
                .ToList();
        }
    }
}
using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IInventoryService
    {
        void AddSupply(MedicalSupply supply);
        List<MedicalSupply> GetAllSupplies();
        MedicalSupply GetSupplyById(string id);
        void UpdateSupply(MedicalSupply supply);
        void AddStock(string id, int qty, string batchNumber = "AUTO", DateTime? expiry = null);
        void RemoveStock(string id, int qty);
        void RemoveMedicineStock(string medId, int qtyToRemove);
        void DeleteSupply(string id, User user);

        List<MedicalSupply> GetLowStockSupplies();

        List<MedicalSupply> GetExpiringSupplies(int days);
        List<MedicalSupply> GetExpiredSupplies();
        List<Transaction> GetAllTransactions();
        List<MedicalSupply> SearchSupplies(string query);
    }
}
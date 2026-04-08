using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IInventoryService
    {
        void AddSupply(MedicalSupply supply);
        List<MedicalSupply> GetAllSupplies();
        MedicalSupply GetSupplyById(string id);
        void UpdateSupply(MedicalSupply supply);
        void AddStock(string id, int qty);
        void RemoveStock(string id, int qty);
        void DeleteSupply(string id);

        List<MedicalSupply> GetLowStockSupplies();

        List<MedicalSupply> GetExpiredItems();
        List<MedicalSupply> GetExpiringSupplies(int days);
        List<Transaction> GetAllTransactions();
    }
}
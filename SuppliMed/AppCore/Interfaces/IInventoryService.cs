using System.Reflection.Metadata;
using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IInventoryService
    {
        void AddSupply(MedicalSupply supply, User user);
        void DeleteSupply(string id, User user);
        List<MedicalSupply> GetAllSupplies();
        MedicalSupply GetSupplyById(string id);
        void UpdateSupply(MedicalSupply supply, User user);
        void AddStock(string id, int qty, User user, string batchNumber = "AUTO", DateTime? expiry = null);
        void RemoveStock(string id, int qty, User user);

        List<MedicalSupply> GetLowStockSupplies();
        List<MedicalSupply> GetExpiredSupplies();
        List<MedicalSupply> GetFilteredExpiringSupplies(int daysThreshold);
        List<MedicalSupply> SearchSupplies(string query);
        List<AuditLog> GetAllAuditLogs();

        int GetLowStockCount();
        int GetExpiredCount();
        string GenerateNextId(string category);
    }
}
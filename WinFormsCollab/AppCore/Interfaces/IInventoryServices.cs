using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IInventoryService
    {
        void AddSupply(MedicalSupply supply);
        List<MedicalSupply> GetAllSupplies();
        MedicalSupply GetSupplyById(string id);
        void UpdateSupply(MedicalSupply supply);
        void DeleteSupply(string id);

        List<MedicalSupply> GetLowStockSupplies();
        List<MedicalSupply> GetExpiringSupplies(int days);
    }
}
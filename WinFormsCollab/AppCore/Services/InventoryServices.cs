using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services
{
    public class InventoryServices : IInventoryService
    {
        private static InventoryServices _instance;
        private static readonly object _lock = new object();

        private List<MedicalSupply> supplies = new List<MedicalSupply>();
        private List<Transaction> transactions = new List<Transaction>();

        // 1. Private constructor with Dummy Data Seeding
        private InventoryServices() 
        { 
            SeedInitialData();
        }

        public static InventoryServices Instance 
        {
            get 
            {
                lock (_lock) 
                {
                    if (_instance == null) _instance = new InventoryServices();
                    return _instance;
                }
            }
        }

        private void SeedInitialData()
        {
            // Adding dummy Medicine
            AddSupply(new Medicine { 
                Id = "MED001", 
                Name = "Paracetamol", 
                CurrentStock = 120, 
                MinimumStock = 100, 
                ExpirationDate = DateTime.Now.AddDays(10) 
            });

            // Adding dummy Equipment
            AddSupply(new Equipment { 
                Id = "EQ001", 
                Name = "Digital Thermometer", 
                CurrentStock = 5, 
                MinimumStock = 10, 
                SerialNumber = "SN-9921"
            });
        }

        public void AddSupply(MedicalSupply supply)
        {
            if (supply == null) return;
            supplies.Add(supply);
        }

        public List<MedicalSupply> GetAllSupplies() => supplies;

        public MedicalSupply GetSupplyById(string id)
        {
            return supplies.FirstOrDefault(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateSupply(MedicalSupply supply)
        {
            var existing = GetSupplyById(supply.Id);
            if (existing != null)
            {
                existing.Name = supply.Name;
                existing.CurrentStock = supply.CurrentStock;
                existing.MinimumStock = supply.MinimumStock;
            }
        }

        // Consolidated Delete: Requires Admin check as per your Flowchart
        public void DeleteSupply(string id, User user)
        {
            if (user is not Admin)
                throw new UnauthorizedAccessException("Only Admins can remove supplies from the registry.");

            var supply = GetSupplyById(id);
            if (supply != null)
                supplies.Remove(supply);
        }

        public void AddStock(string id, int qty)
        {
            var supply = GetSupplyById(id);
            if (supply != null)
            {
                supply.AddStock(qty);
                RecordTransaction(supply, qty, "RESTOCK");
            }
        }

        public void RemoveStock(string id, int qty)
        {
            var supply = GetSupplyById(id);
            if (supply != null)
            {
                supply.ReduceStock(qty);
                RecordTransaction(supply, qty, "DISPENSE");
            }
        }

        public List<MedicalSupply> GetLowStockSupplies()
        {
            // Uses the method from your MedicalSupply base class
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
        
        public List<MedicalSupply> GetExpiredSupplies()
        {
            return supplies
                .OfType<Medicine>()
                .Where(m => m.IsExpired())
                .Cast<MedicalSupply>()
                .ToList();
        }

        // Improved Transaction recording to link the object
        private void RecordTransaction(MedicalSupply supply, int qty, string type)
        {
            transactions.Add(new Transaction
            {
                // Generate a unique ID for the history log
                TransactionID = "TXN-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                AddedBy = "abc123", // Placeholder for user ID, ideally should be passed as a parameter
                Date = DateTime.Now,
                Quantity = qty,
                Type = $"{type}: {supply.Name} ({supply.Id})"
            });
        }

        public List<Transaction> GetAllTransactions() => transactions;

        public int GetTotalSupplyCount() => supplies.Count;

        public List<MedicalSupply> SearchSupplies(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return supplies;

            return supplies.Where(s => 
                s.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                s.Id.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                s.Brand.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}

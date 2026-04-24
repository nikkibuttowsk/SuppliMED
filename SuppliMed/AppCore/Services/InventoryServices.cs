using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services
{
    public class InventoryServices : IInventoryService
    {
        private static InventoryServices? _instance = null;
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
            var paracetamol = new Medicine { 
                Id = "MED001", 
                Name = "Paracetamol", 
                Brand = "Biogesic",
                MinimumStock = 100 
            };

            // Add stock batches
            paracetamol.Batches.Add(new Batch { 
                BatchNumber = "B-001", 
                Quantity = 120, 
                ExpirationDate = DateTime.Now.AddDays(10) 
            });

            AddSupply(paracetamol);

            // Adding dummy Equipment
            AddSupply(new Equipment { 
                Id = "EQ001", 
                Name = "Digital Thermometer", 
                Quantity = 5, 
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
                existing.Quantity = supply.Quantity;
                existing.MinimumStock = supply.MinimumStock;
            }
        }

        public void DeleteSupply(string id, User user)
        {
            if (user is not Admin)
                throw new UnauthorizedAccessException("Only Admins can remove supplies from the registry.");

            var supply = GetSupplyById(id);
            if (supply != null)
                supplies.Remove(supply);
        }

        public void AddStock(string id, int qty, string batchNumber = "AUTO", DateTime? expiry = null)
        {
            var supply = GetSupplyById(id);
            if (supply == null) return;

            if (supply is Medicine med)
            {
                med.Batches.Add(new Batch 
                { 
                    BatchNumber = batchNumber, 
                    Quantity = qty, 
                    ExpirationDate = expiry ?? DateTime.Now.AddMonths(6)
                });
            }
            else
            {
                supply.AddStock(qty);
            }

            RecordTransaction(supply, qty, "RESTOCK", "System");
        }

        public void RemoveStock(string id, int qty)
        {
            var supply = GetSupplyById(id);
            if (supply == null) return;

            if (supply is Medicine med)
            {
                int remainingToRemove = qty;
                
                // sort batches by expiration date 
                var sortedBatches = med.Batches.OrderBy(b => b.ExpirationDate).ToList();

                foreach (var batch in sortedBatches)
                {
                    if (remainingToRemove <= 0) break;

                    if (batch.Quantity >= remainingToRemove)
                    {
                        batch.Quantity -= remainingToRemove;
                        remainingToRemove = 0;
                    }
                    else
                    {
                        remainingToRemove -= batch.Quantity;
                        batch.Quantity = 0;
                    }
                }
                
                med.Batches.RemoveAll(b => b.Quantity <= 0);
                
            }
            else
            {
                supply.ReduceStock(qty);
            }

            RecordTransaction(supply, qty, "DISPENSE", "System");
        }

        public void RemoveMedicineStock(string medId, int qtyToRemove)
        {
            var med = GetSupplyById(medId) as Medicine;
            if (med == null) return;

            // Sort batches by expiration date (FEFO)
            var activeBatches = med.Batches.OrderBy(b => b.ExpirationDate).ToList();

            foreach (var batch in activeBatches)
            {
                if (qtyToRemove <= 0) break;

                if (batch.Quantity >= qtyToRemove) {
                    batch.Quantity -= qtyToRemove;
                    qtyToRemove = 0;
                } else {
                    qtyToRemove -= batch.Quantity;
                    batch.Quantity = 0;
                }
            }
            // clean empty batches
            med.Batches.RemoveAll(b => b.Quantity <= 0);
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
                .Where(m => m.IsAnyBatchExpired())
                .Cast<MedicalSupply>()
                .ToList();
        }

        // Improved Transaction recording to link the object
        private void RecordTransaction(MedicalSupply supply, int qty, string type, string userName)
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
                (s.Brand?? "").Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        public List<MedicalSupply> GetFilteredExpiringSupplies(int daysThreshold = 30)
        {
            return supplies
                .OfType<Medicine>()
                .Where(m => m.IsExpiringSoon(daysThreshold))
                .Cast<MedicalSupply>()
                .ToList();
        }
    }
}

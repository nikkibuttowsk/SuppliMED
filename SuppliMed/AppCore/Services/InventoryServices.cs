using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Data;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Services
{
    public class InventoryServices : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryServices(AppDbContext context)
        {
            _context = context;
        }

        // --- CREATE ---
        public void AddSupply(MedicalSupply supply)
        {
            if (supply == null) return;
            _context.Supplies.Add(supply);
            RecordTransaction(supply, supply.Quantity, "ADD");
            _context.SaveChanges();
        }

        // --- READ ---
        public List<MedicalSupply> GetAllSupplies()
        {
            return _context.Supplies
                .Include(s => s.Batches)
                .ToList();
        }

        // Removed the '?' to match IInventoryService contract and used '!' to handle nullability
        public MedicalSupply GetSupplyById(string id)
        {
            var supply = _context.Supplies
                .Include(s => s.Batches)
                .FirstOrDefault(s => s.Id == id);
            
            return supply!; 
        }

        // --- UPDATE ---
        public void UpdateSupply(MedicalSupply supply)
        {
            var existing = GetSupplyById(supply.Id);
            if (existing != null)
            {
                existing.Name = supply.Name;
                existing.MinimumStock = supply.MinimumStock;
                existing.Brand = supply.Brand;
                existing.Category = supply.Category;
                
                if (existing is not Medicine)
                {
                    existing.Quantity = supply.Quantity;
                }
                
                _context.SaveChanges();
            }
        }

        // --- DELETE ---
        public void DeleteSupply(string id, User user)
        {
            if (user is not Admin)
                throw new UnauthorizedAccessException("Only Admins can remove supplies from the registry.");

            var supply = _context.Supplies.Find(id);
            if (supply != null)
            {
                RecordTransaction(supply, 0, "DELETE");
                _context.Supplies.Remove(supply);
                _context.SaveChanges();
            }
        }

        // --- INTERFACE IMPLEMENTATIONS (Add/Remove Stock) ---
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
                    ExpirationDate = expiry ?? DateTime.Now.AddMonths(6),
                    MedicalSupplyId = supply.Id
                });
            }
            else
            {
                supply.AddStock(qty);
            }

            RecordTransaction(supply, qty, "RESTOCK");
            _context.SaveChanges();
        }

        public void RemoveStock(string id, int qty)
        {
            var supply = GetSupplyById(id) as MedicalSupply;
            if (supply == null) return;

            if (supply is Medicine med)
            {
                RemoveMedicineStockInternal(med, qty);
            }
            else
            {
                supply.ReduceStock(qty);
            }

            RecordTransaction(supply, qty, "DISPENSE");
            _context.SaveChanges();
        }

        public void RemoveMedicineStock(string medId, int qtyToRemove)
        {
            var med = GetSupplyById(medId) as Medicine;
            if (med != null)
            {
                RemoveMedicineStockInternal(med, qtyToRemove);
                _context.SaveChanges();
            }
        }

        // Private Helper for FEFO Logic
        private void RemoveMedicineStockInternal(Medicine med, int qtyToRemove)
        {
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

            var emptyBatches = med.Batches.Where(b => b.Quantity <= 0).ToList();
            foreach (var batch in emptyBatches)
            {
                med.Batches.Remove(batch);
            }
        }

        // --- QUERIES & ANALYTICS ---
        public List<MedicalSupply> GetLowStockSupplies()
        {
            return GetAllSupplies()
                .Where(s => s.IsLowStock())
                .OrderBy(s => s.Quantity)
                .ToList();
        }

        public List<MedicalSupply> GetExpiringSupplies(int days)
        {
            return _context.Supplies
                .OfType<Medicine>()
                .Include(m => m.Batches)
                .AsEnumerable() 
                .Where(m => 
                    m.GetNextExpirationDate() != null &&
                    !m.IsAnyBatchExpired() &&
                    m.IsExpiringSoon(days))
                .Cast<MedicalSupply>()
                .ToList();
        }

        public List<MedicalSupply> GetExpiredSupplies()
        {
            return _context.Supplies
                .OfType<Medicine>()
                .Include(m => m.Batches)
                .AsEnumerable()
                .Where(m => m.IsAnyBatchExpired())
                .Cast<MedicalSupply>()
                .ToList();
        }

        public List<MedicalSupply> GetFilteredExpiringSupplies(int daysThreshold = 30)
        {
            var threshold = DateTime.Now.AddDays(daysThreshold);
                return _context.Supplies
                    .OfType<Medicine>()
                    .Include(m => m.Batches)
                    .AsEnumerable()
                    .Where(m => 
                            m.GetNextExpirationDate() != null &&
                            m.GetNextExpirationDate() > DateTime.Now && 
                            (m.GetNextExpirationDate().Value - DateTime.Now).TotalDays <= daysThreshold)
                    .Cast<MedicalSupply>()
                    .ToList();
        }


        public List<MedicalSupply> SearchSupplies(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return GetAllSupplies();

            return _context.Supplies
                .Where(s => s.Name.Contains(query) || 
                            s.Id.Contains(query) ||
                            (s.Brand != null && s.Brand.Contains(query)))
                .ToList();
        }

        public int GetTotalSupplyCount() => _context.Supplies.Count();
        public int GetLowStockCount() => GetLowStockSupplies().Count;
        public int GetExpiredCount() => GetExpiredSupplies().Count;

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions
                .OrderByDescending(t => t.DateTime) //most recent action appear at the top
                .ToList(); 
        }

        private void RecordTransaction(MedicalSupply supply, int qty, string type)
        {
            var transaction = new Transaction
            {
                Action = type,
                Item = supply.Name,
                DateTime = DateTime.Now,
                User = "Admin", // For now, hardcode or get from session
                Details = type switch
                {
                    "ADD" => $"Registered new supply with {qty} units.",
                    "DELETE" => $"Removed item {supply.Id} from registry.",
                    "RESTOCK" => $"Increased stock by {qty}.",
                    "DISPENSE" => $"Decreased stock by {Math.Abs(qty)}.",
                    _ => $"Updated {supply.Name}"
                }
            };

            _context.Transactions.Add(transaction);
        }

        // Unified method often used by Controllers
        public void ProcessStockUpdate(string id, int qty, string? batchNumber = "AUTO")
        {
            if (qty > 0) AddStock(id, qty, batchNumber ?? "AUTO");
            else if (qty < 0) RemoveStock(id, Math.Abs(qty));
        }

        public string GenerateNextId(string category)
        {
            var supplies = GetAllSupplies();

            string prefix = category == "Medicine" ? "MED" : "EQP";

            var numbers = supplies
                .Where(s => s.Id.StartsWith(prefix))
                .Select(s => int.TryParse(s.Id.Substring(3), out int num) ? num : 0);

            int next = numbers.Any() ? numbers.Max() + 1 : 1;

            return $"{prefix}{next:D3}"; // MED001, EQP001
        }
    }
}
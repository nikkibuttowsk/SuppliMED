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
        public void AddSupply(MedicalSupply supply, User user)
        {
            if (user is not Admin) throw new UnauthorizedAccessException("Only Admins can add new supplies.");
            _context.Supplies.Add(supply);
            RecordAuditLog(supply, supply.Quantity, "ADD", user);
            _context.SaveChanges();
        }

        public List<MedicalSupply> GetAllSupplies()
        {
            return _context.Supplies.Include(s => s.Batches).ToList();
        }

        public MedicalSupply GetSupplyById(string id)
        {
            var supply = _context.Supplies
                .Include(s => s.Batches)
                .FirstOrDefault(s => s.Id == id);
            
            return supply!; 
        }

        // --- UPDATE ---
        public void UpdateSupply(MedicalSupply supply, User user)
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

                RecordAuditLog(existing, 0, "UPDATE", user);
                _context.SaveChanges();
            }
        }

        // --- DELETE ---
        public void DeleteSupply(string id, User user)
        {
            if (user is not Admin) throw new UnauthorizedAccessException("Only Admins can delete supplies.");
            var supply = _context.Supplies.Find(id);
            if (supply != null)
            {
                RecordAuditLog(supply, 0, "DELETE", user);
                _context.Supplies.Remove(supply);
                _context.SaveChanges();
            }
        }

        public void AddStock(string id, int qty, User user, string batchNumber = "AUTO", DateTime? expiry = null)
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

            RecordAuditLog(supply, qty, "RESTOCK", user);
            _context.SaveChanges();
        }

        public void RemoveStock(string id, int qty, User user)
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

            RecordAuditLog(supply, qty, "DISPENSE", user);
            _context.SaveChanges();
        }

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
                _context.Batches.Remove(batch);
                med.Batches.Remove(batch);
            } 
        }

        // --- AUDIT LOGS ---
        public List<AuditLog> GetAllAuditLogs()
        {
            return _context.AuditLogs
                .OrderByDescending(t => t.DateTime)
                .ToList(); 
        }

        // FIXED: Added User parameter and correct argument types
        private void RecordAuditLog(MedicalSupply supply, int qty, string type, User user)
        {
            var auditLog = new AuditLog
            {
                LogId = Guid.NewGuid().ToString(),
                DateTime = DateTime.Now,
                User = user.Username ?? "Unknown",
                Action = type,
                Item = supply.Name,
                Details = type switch
                {
                    "ADD" => $"Registered new supply with {qty} units.",
                    "DELETE" => $"Removed item {supply.Id} from registry.",
                    "RESTOCK" => $"Increased stock by {qty}.",
                    "DISPENSE" => $"Decreased stock by {Math.Abs(qty)}.",
                    "UPDATE" => $"Updated basic info for {supply.Name}.",
                    _ => $"Action {type} performed on {supply.Name}"
                }
            };
            _context.AuditLogs.Add(auditLog);
        }

        // --- QUERIES ---
        public List<MedicalSupply> GetLowStockSupplies() => GetAllSupplies().Where(s => s.IsLowStock()).ToList();
        public int GetLowStockCount() => GetLowStockSupplies().Count;

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

        public int GetExpiredCount() => GetExpiredSupplies().Count;

        public List<MedicalSupply> GetFilteredExpiringSupplies(int daysThreshold = 30)
        {
            var today = DateTime.Now;
            return _context.Supplies
                .OfType<Medicine>()
                .Include(m => m.Batches)
                .AsEnumerable()
                .Where(m => {
                    DateTime? nextExpiry = m.GetNextExpirationDate();
                    return nextExpiry.HasValue && 
                        nextExpiry.Value > today && 
                        (nextExpiry.Value - today).TotalDays <= daysThreshold;
                })
                .Cast<MedicalSupply>()
                .ToList();
        }

        // FIXED: Added User parameter to satisfy AddStock/RemoveStock calls
        public void ProcessStockUpdate(string id, int qty, User user, string? batchNumber = "AUTO")
        {
            if (qty > 0) AddStock(id, qty, user, batchNumber ?? "AUTO");
            else if (qty < 0) RemoveStock(id, Math.Abs(qty), user);
        }

        public string GenerateNextId(string category)
        {
            string prefix = category == "Medicine" ? "MED" : "EQP";
            var numbers = GetAllSupplies().Where(s => s.Id.StartsWith(prefix))
                .Select(s => int.TryParse(s.Id.Substring(3), out int num) ? num : 0);
            return $"{prefix}{(numbers.Any() ? numbers.Max() + 1 : 1):D3}";
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
    }
}
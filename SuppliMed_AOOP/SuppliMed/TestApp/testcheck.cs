using System;
using System.Linq;
using AppCore.Models;
using AppCore.Services;
using AppCore.Interfaces;

class Program
{
    static void Main()
    {
        IInventoryService service = InventoryServices.Instance;

        Console.WriteLine("=== STARTING SYSTEM TEST: BATCH & FEFO LOGIC ===\n");

        // Batch A: expiring very soon (should be used first)
        // Batch B: fresh stock (should be used second)
        var med = new Medicine 
        { 
            Id = "MED-TEST", 
            Name = "Amoxicillin", 
            Brand = "Generic", 
            MinimumStock = 20 
        };

        med.Batches.Add(new Batch { BatchNumber = "OLD-001", Quantity = 10, ExpirationDate = DateTime.Now.AddDays(2) });
        med.Batches.Add(new Batch { BatchNumber = "NEW-999", Quantity = 50, ExpirationDate = DateTime.Now.AddMonths(12) });

        service.AddSupply(med);

        // dashboard checks
        Console.WriteLine($"Total Supplies in System: {service.GetAllSupplies().Count}");
        
        var expiringSoon = service.GetExpiringSupplies(5);
        Console.WriteLine($"> Supplies Expiring in 5 days: {expiringSoon.Count} ({expiringSoon.FirstOrDefault()?.Name})");

        // check removing 15 units. 
        Console.WriteLine("\n--- Action: Dispensing 15 units of Amoxicillin ---");
        service.RemoveStock("MED-TEST", 15);

        var updatedMed = service.GetSupplyById("MED-TEST") as Medicine;
        
        if (updatedMed != null)
        {
            Console.WriteLine($"Total Stock Remaining: {updatedMed.CurrentStock}");
            Console.WriteLine($"Number of Batches left: {updatedMed.Batches.Count}");
            
            foreach(var b in updatedMed.Batches)
            {
                Console.WriteLine($"> Remaining Batch: {b.BatchNumber} | Qty: {b.Quantity} | Expiry: {b.ExpirationDate:yyyy-MM-dd}");
            }
        }

        // 5. Test: Transaction History
        Console.WriteLine("\n--- Transaction Log ---");
        foreach (var txn in service.GetAllTransactions())
        {
            Console.WriteLine($"[{txn.Date:HH:mm:ss}] {txn.Type} | Qty: {txn.Quantity} | User: {txn.AddedBy}");
        }

        // 6. Test: Low Stock Detection
        var lowStock = service.GetLowStockSupplies();
        Console.WriteLine($"\nLow Stock Alerts: {lowStock.Count}");
        foreach(var item in lowStock)
        {
            Console.WriteLine($"> ALERT: {item.Name} is below minimum stock!");
        }

        Console.WriteLine("\n=== TEST COMPLETE ===");
    }
}
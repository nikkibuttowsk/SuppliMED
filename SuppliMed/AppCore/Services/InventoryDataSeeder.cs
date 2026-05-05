using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Models;
using AppCore.Data;

namespace AppCore.Services;

public static class InventoryDataSeeder
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Supplies.Any()) return;

        var items = new List<MedicalSupply>
        {
            new Medicine { 
                Id = "MED001", 
                Name = "Ibuprofen", 
                Brand = "Advil", 
                Category = "Analgesic",
                Quantity = 100, // Updated to match your model's Quantity field
                MinimumStock = 10
            },
            new Equipment { 
                Id = "EQ001", 
                Name = "Stethoscope", 
                Brand = "Littmann", 
                Category = "Diagnostic",
                Quantity = 10,
                MinimumStock = 2,
                SerialNumber = "SN12345"
            }
        };

        context.Supplies.AddRange(items);

        context.AuditLogs.Add(new AuditLog 
        { 
            LogId = Guid.NewGuid().ToString(),
            DateTime = DateTime.Now, 
            User = "System Admin",
            Action = "SEED",
            Item = "Database",
            Details = "Initial inventory data seeded successfully."
        });

        context.SaveChanges();
    }
}
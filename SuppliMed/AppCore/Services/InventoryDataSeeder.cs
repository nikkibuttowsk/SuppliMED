using System;
using System.Collections.Generic;
using AppCore.Models;

namespace AppCore.Services;

public static class InventoryDataSeeder
{
    public static List<MedicalSupply> GetSeedData()
    {
        var supplies = new List<MedicalSupply>();

        var paracetamol = new Medicine { 
            Id = "MED001", 
            Name = "Paracetamol", 
            Brand = "Biogesic",
            MinimumStock = 100 
        };
        paracetamol.Batches.Add(new Batch { 
            BatchNumber = "B-001", 
            Quantity = 120, 
            ExpirationDate = DateTime.Now.AddDays(10) 
        });
        supplies.Add(paracetamol);

        supplies.Add(new Equipment { 
            Id = "EQ001", 
            Name = "Digital Thermometer", 
            Quantity = 5, 
            MinimumStock = 10, 
            SerialNumber = "SN-9921"
        });

        supplies.Add(new Equipment { 
            Id = "EQ002", 
            Name = "Nebulizer", 
            Quantity = 2, 
            MinimumStock = 5, 
            SerialNumber = "SN-9925"
        });

        return supplies;
    }
}
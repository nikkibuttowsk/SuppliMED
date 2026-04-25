using System;
using System.Collections.Generic;
using AppCore.Models;
using AppCore.Data;

namespace AppCore.Services;

public static class InventoryDataSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Supplies.Any()) return;

        var items = new List<MedicalSupply>
        {
            new Medicine { Id = "MED001", Name = "Ibuprofen", Brand = "Advil", Category = "Analgesic" },
            new Equipment { Id = "EQ001", Name = "Stethoscope", Brand = "Littmann", Category = "Diagnostic" }
        };

        context.Supplies.AddRange(items);
        context.SaveChanges();
    }
}
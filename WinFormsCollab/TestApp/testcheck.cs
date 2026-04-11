using System;
using AppCore.Models;
using AppCore.Services;

class Program
{
    static void Main()
    {
        InventoryServices service = new InventoryServices();

        var med1 = new Medicine
        {
            Id = "001",
            Name = "Paracetamol",
            Quantity = 50,
            MinimumStock = 100,
            ExpirationDate = DateTime.Now.AddDays(-5)
        };
        var med2 = new Medicine
        {
            Id = "002",
            Name = "Biogesic",
            Quantity = 30,
            MinimumStock = 100,
            ExpirationDate = DateTime.Now.AddDays(10)
        };

        service.AddSupply(med1);
        service.AddSupply(med2);

        var expiredSupplies = service.GetExpiredSupplies();

        foreach (var item in expiredSupplies)
        {
            Console.WriteLine($"{item.Name} is EXPIRED");
        }
    }
}
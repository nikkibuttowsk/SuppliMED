using System;
using AppCore.Models;
using AppCore.Services;

class Program
{
    static void Main()
    {
        InventoryServices service = new InventoryServices();

        var med = new Medicine
        {
            Id = "001",
            Name = "Paracetamol",
            Quantity = 50,
            MinimumStock = 100,
            ExpirationDate = DateTime.Now.AddDays(10)
        };

        service.AddSupply(med);

        foreach (var item in service.GetAllSupplies())
        {
            Console.WriteLine($"{item.Name} - {item.GetDetails()}");
        }
    }
}
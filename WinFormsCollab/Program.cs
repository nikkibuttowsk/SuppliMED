using System;
using AppCore.Models;
using AppCore.Services;

class Program
{
    static void Main()
    {
        InventoryService service = new InventoryService();

        var med = new Medicine
        {
            Id = "001",
            Name = "Paracetamol",
            CurrentStock = 50,
            MinStock = 100,
            ExpirationDate = DateTime.Now.AddDays(10)
        };

        service.AddItem(med);

        foreach (var item in service.GetAllItems())
        {
            Console.WriteLine($"{item.Name} - {item.GetStatus()}");
        }
    }
}
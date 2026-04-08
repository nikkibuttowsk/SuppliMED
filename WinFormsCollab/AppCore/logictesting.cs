var service = new InventoryService();

service.AddSupply(new MedicalSupply
{
    Id = "001",
    Name = "Paracetamol",
    Category = "Medicine",
    Quantity = 50,
    MinimumStock = 10,
    ExpirationDate = DateTime.Now.AddMonths(6)
});

var all = service.GetAllSupplies();
Console.WriteLine(all.Count);
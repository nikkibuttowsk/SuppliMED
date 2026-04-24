using AppCore.Interfaces; 
using AppCore.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllers();


builder.Services.AddSingleton<IInventoryService>(sp => InventoryServices.Instance);
builder.Services.AddSingleton<AppCore.Services.InventoryServices>(sp => 
    AppCore.Services.InventoryServices.Instance);

var app = builder.Build();


// 2. CONFIGURE MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseDefaultFiles(); 
app.UseStaticFiles();  

app.Run();
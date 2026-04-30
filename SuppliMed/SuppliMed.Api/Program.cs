using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using AppCore.Interfaces; 
using AppCore.Services;

var builder = WebApplication.CreateBuilder(args);

// connection string once
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

// services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // This is crucial for Polymorphic (Medicine/Equipment) data
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = 
            System.Text.Json.JsonNamingPolicy.CamelCase;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<InventoryServices>();

builder.Services.AddScoped<IInventoryService, InventoryServices>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ Static files (optional for frontend)
app.UseDefaultFiles(); 
app.UseStaticFiles();  

app.MapControllers();

// ✅ Seed database safely
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    InventoryDataSeeder.Seed(context);
}

app.Run();
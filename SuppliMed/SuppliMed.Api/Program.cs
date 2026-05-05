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

builder.Services.AddDistributedMemoryCache(); // Required for session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session expiration
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<InventoryServices>();

builder.Services.AddScoped<IInventoryService, InventoryServices>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.WithOrigins("http://localhost:5000", "http://127.0.0.1:5500") // Add your frontend URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Required for Sessions to work
    });
});

var app = builder.Build();

// 1. Static files should be outside the HTTPS/Auth logic for performance
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. Add Routing before CORS and Session
app.UseRouting();

// 3. CORS must come BEFORE Session and Authorization
app.UseCors("AllowAll");

// 4. Session must come AFTER Routing/CORS but BEFORE MapControllers
app.UseSession();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Seeder logic...
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    InventoryDataSeeder.Seed(context);
}

app.Run();
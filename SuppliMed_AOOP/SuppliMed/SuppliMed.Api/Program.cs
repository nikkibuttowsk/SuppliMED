using AppCore.Interfaces; 
using AppCore.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();


builder.Services.AddSingleton<IInventoryService>(sp => InventoryServices.Instance);

var app = builder.Build();

// 2. CONFIGURE MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseDefaultFiles(); 
app.UseStaticFiles();  

// app.MapPost("/api/login", (LoginRequest data) =>
// {
//     var result = AppCore.Services.AuthService.Login(data.Username, data.Password);

//     switch (result.Status)
//     {
//         case AppCore.Services.AuthService.LoginResultStatus.Success:
//             return Results.Ok(new { message = "Welcome back!" });

//         case AppCore.Services.AuthService.LoginResultStatus.LockedOut:
//             // Return 423 Locked for proper client handling
//             return Results.Json(new { 
//                 message = $"Too many attempts. Account frozen.", 
//                 secondsRemaining = result.TimeRemainingSeconds 
//             }, statusCode: 423);

//         case AppCore.Services.AuthService.LoginResultStatus.Failure:
//         default:
//             return Results.Unauthorized(); 
//     }
// });

app.Run();
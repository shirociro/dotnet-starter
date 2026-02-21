using Microsoft.EntityFrameworkCore;
using netcore.Data;
using netcore.Modules.Users;
using netcore.Modules.Tasks;

var builder = WebApplication.CreateBuilder(args);

// 1. DATABASE LOGIC: Handle Render's postgres:// URL
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString;

if (!string.IsNullOrEmpty(databaseUrl))
{
    // Convert Render's postgres:// format to Npgsql format
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var port = uri.Port <= 0 ? 5432 : uri.Port;
    connectionString = $"Host={uri.Host};Port={port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;";
}
else
{
    // Fallback for local development
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                        ?? "Host=localhost;Database=testdb;Username=postgres;Password=password";
}

// Add services
builder.Services.AddControllers();

// 2. Use the connectionString we just built
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TaskService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// // 3. AUTO-MIGRATE: The "Clean Slate" Version
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     try
//     {
//         var context = services.GetRequiredService<AppDbContext>();
        
//         // --- THE RESET FIX ---
//         // This clears the "history" table that's blocking your new table creation
//         // Note: You can remove the 'EnsureDeleted' line after the first successful run
//         Console.WriteLine("--> Wiping database for a clean start...");
//         context.Database.EnsureDeleted(); 
        
//         context.Database.Migrate();
//         Console.WriteLine("--> Database migration successful! Tables created.");
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"--> Migration Error: {ex.Message}");
//     }
// }

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection(); // Keep disabled if testing with raw HTTP curl
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<YourDbContextName>(); // Change to your actual context name
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}
app.Run();
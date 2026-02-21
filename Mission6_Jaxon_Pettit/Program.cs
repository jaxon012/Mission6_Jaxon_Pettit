using Microsoft.EntityFrameworkCore;
using Mission6_Jaxon_Pettit.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// EF Core + SQLite
// IMPORTANT: make sure this name EXACTLY matches appsettings.json
var connString = builder.Configuration.GetConnectionString("MovieCollectionConnection");

if (string.IsNullOrWhiteSpace(connString))
{
    throw new Exception("Connection string 'MovieCollectionConnection' not found in appsettings.json.");
}

builder.Services.AddDbContext<MovieCollectionContext>(options =>
    options.UseSqlite(connString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieCollectionContext>();
    // db.Database.Migrate(); // <-- keep OFF
    // No seeding here
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection(); // optional
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
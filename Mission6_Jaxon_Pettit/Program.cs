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

// Auto-migrate + seed on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieCollectionContext>();
    db.Database.Migrate();

    // Seed only once
    if (!db.Movies.Any())
    {
        db.Movies.AddRange(
            new Movie
            {
                Category = "Drama",
                Title = "Interstellar",
                Year = 2014,
                Director = "Christopher Nolan",
                Rating = "PG-13",
                Edited = false
            },
            new Movie
            {
                Category = "Action",
                Title = "The Dark Knight",
                Year = 2008,
                Director = "Christopher Nolan",
                Rating = "PG-13",
                Edited = false
            },
            new Movie
            {
                Category = "Comedy",
                Title = "The Princess Bride",
                Year = 1987,
                Director = "Rob Reiner",
                Rating = "PG",
                Edited = false
            }
        );

        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// If you don't have HTTPS configured, it's fine to leave this off.
// app.UseHttpsRedirection();

// REQUIRED for wwwroot (images/css/js)
app.UseStaticFiles();

app.UseRouting();

// optional but standard
app.UseAuthorization();

// REQUIRED for controller/action routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

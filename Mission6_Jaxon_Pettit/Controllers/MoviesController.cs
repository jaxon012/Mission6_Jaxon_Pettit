using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission6_Jaxon_Pettit.Models;

namespace Mission6_Jaxon_Pettit.Controllers;

[Route("Movies")]
public class MoviesController : Controller
{
    private readonly MovieCollectionContext _context;

    public MoviesController(MovieCollectionContext context)
    {
        _context = context;
    }

    // GET: /Movies
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies
            .AsNoTracking()
            .OrderBy(m => m.Title)
            .ToListAsync();

        return View(movies);
    }

    // GET: /Movies/Add
    [HttpGet("Add")]
    public IActionResult Add()
    {
        return View(new Movie { Rating = "None" });
    }

    // POST: /Movies/Add
    [HttpPost("Add")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Movie movie)
    {
        movie.Rating = string.IsNullOrWhiteSpace(movie.Rating) ? "None" : movie.Rating;

        if (!ModelState.IsValid)
        {
            return View(movie);
        }

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            .Include(m => m.Category)
            .AsNoTracking()
            .OrderBy(m => m.Title)
            .ToListAsync();

        return View(movies);
    }

    // GET: /Movies/Add
    [HttpGet("Add")]
    public async Task<IActionResult> Add()
    {
        await PopulateCategoriesDropDownList();
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
            await PopulateCategoriesDropDownList(movie.CategoryId);
            return View(movie);
        }

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    // GET: /Movies/Edit/5
    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null)
        {
            return NotFound();
        }

        await PopulateCategoriesDropDownList(movie.CategoryId);
        return View(movie);
    }

// POST: /Movies/Edit/5
    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movie movie)
    {
        if (id != movie.MovieId)
        {
            return BadRequest();
        }

        movie.Rating = string.IsNullOrWhiteSpace(movie.Rating) ? "None" : movie.Rating;

        if (!ModelState.IsValid)
        {
            await PopulateCategoriesDropDownList(movie.CategoryId);
            return View(movie);
        }

        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

// GET: /Movies/Delete/5
    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

// POST: /Movies/Delete/5
    [HttpPost("Delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Helper: loads categories for the dropdown in Add/Edit views
    private async Task PopulateCategoriesDropDownList(int? selectedCategoryId = null)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", selectedCategoryId);
    }
}
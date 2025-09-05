using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MovieShop.MVC.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;
    private readonly MovieShopDbContext _db;
    private readonly IPurchaseService _purchaseService;
    public MoviesController(IMovieService movieService, MovieShopDbContext db, IPurchaseService purchaseService)
    {
        _movieService = movieService;
        _db = db;
        _purchaseService = purchaseService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await _movieService.GetMovieDetailsAsync(id);
        if (model == null) return NotFound();
        ViewBag.Price = await _movieService.GetPriceAsync(id);
        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            model.IsPurchased = await _purchaseService.IsMoviePurchasedAsync(id, userId);
        }
        else
        {
            model.IsPurchased = false;
        }
        return View(model);
    }
    
    [HttpGet("movies/genre/{id:int}")]
    [HttpGet("genres/{id:int}")]
    public async Task<IActionResult> Genre(int id, int page = 1)
    {
        var model = await _movieService.GetByGenreAsync(id, 30, page);
        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Purchase(int movieId, decimal price)
    {
        // get current user id from cookie
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // optional: prevent duplicates
        var already = await _db.Purchases
            .AnyAsync(p => p.UserId == userId && p.MovieId == movieId);
        if (already)
        {
            TempData["Error"] = "You already purchased this movie.";
            return RedirectToAction(nameof(Details), new { id = movieId });
        }

        _db.Purchases.Add(new Purchase
        {
            UserId = userId,
            MovieId = movieId,
            PurchaseDateTime = DateTime.UtcNow,
            PurchaseNumber = Guid.NewGuid(),
            TotalPrice = price
        });

        await _db.SaveChangesAsync();

        TempData["Success"] = "Thanks! Purchase complete.";
        return RedirectToAction(nameof(Details), new { id = movieId });
    }

    [HttpGet("/search")]
    public async Task<IActionResult> Search(string q, int page = 1)
    {
        q = (q ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(q))
            return RedirectToAction("Index", "Home");

        var model = await _movieService.SearchAsync(q, 30, page);
        ViewBag.Query = q;
        return View(model);
    }
}

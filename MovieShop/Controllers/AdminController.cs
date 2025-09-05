using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IMovieService _movieService;

    public AdminController(IAdminService adminService, IMovieService movieService)
    {
        _adminService = adminService;
        _movieService = movieService;
    }

    // /Admin/TopMovies?from=yyyy-MM-dd&to=yyyy-MM-dd&page=1&pageSize=30
    public async Task<IActionResult> TopMovies(DateTime? from, DateTime? to, int page = 1, int pageSize = 30)
    {
        var model = await _adminService.GetTopPurchasedMoviesAsync(from, to, pageSize, page);

        ViewBag.From = from?.ToString("yyyy-MM-dd");
        ViewBag.To = to?.ToString("yyyy-MM-dd");

        return View(model);   // model is PagedResultSet<AdminTopMovieModel>
    }

    [HttpGet]
    public IActionResult CreateMovie() => View(new CreateMovieModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMovie(CreateMovieModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _adminService.CreateMovieAsync(model);
        TempData["Message"] = "Movie created.";
        return RedirectToAction(nameof(TopMovies));
    }
}

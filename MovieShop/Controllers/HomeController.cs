using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IMovieService _movieService;
    public HomeController(IMovieService movieService) => _movieService = movieService;

    public async Task<IActionResult> Index()
    {
        var model = await _movieService.GetTopGrossingAsync(30);
        return View(model);
    }
}

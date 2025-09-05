using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.ViewComponents;

public class GenresViewComponent : ViewComponent
{
    private readonly IGenreService _genreService;
    public GenresViewComponent(IGenreService genreService) => _genreService = genreService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var genres = await _genreService.GetAllAsync();
        return View(genres);
    }
}

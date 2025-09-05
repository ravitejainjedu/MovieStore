using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers;

public class CastController : Controller
{
    private readonly ICastService _castService;
    public CastController(ICastService castService) => _castService = castService;

    public async Task<IActionResult> Details(int id)
    {
        var model = await _castService.GetCastDetailsAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }
}

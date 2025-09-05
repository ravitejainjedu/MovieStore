using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ApplicationCore.Contracts.Services;

[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService) => _userService = userService;

    [Authorize]
    public async Task<IActionResult> Favorites(int page = 1, int pageSize = 30)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var model = await _userService.GetFavoritesAsync(userId, pageSize, page);
        return View(model);
    }

    public IActionResult Account()
    {
        return RedirectToAction("Profile", "Account");
    }
}

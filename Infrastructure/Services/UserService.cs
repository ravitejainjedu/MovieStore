using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly MovieShopDbContext _db;

    public UserService(IPurchaseRepository purchaseRepository, MovieShopDbContext db)
    {
        _purchaseRepository = purchaseRepository;
        _db = db;
    }
    
    public async Task<IReadOnlyList<PurchaseDetailsModel>> GetUserPurchasesAsync(int userId)
    {
        var purchases = await _purchaseRepository.GetByUserAsync(userId);
        return purchases.Select(p => new PurchaseDetailsModel
        {
            MovieId = p.MovieId,
            Title = p.Movie.Title,
            PosterUrl = p.Movie.PosterUrl,
            PurchaseDate = p.PurchaseDateTime,
            Price = p.TotalPrice,
            PurchaseNumber = p.PurchaseNumber
        }).ToList();
    }

    public async Task<PurchaseDetailsModel?> GetPurchaseDetailsAsync(int userId, int movieId)
    {
        var p = await _purchaseRepository.GetByUserAndMovieAsync(userId, movieId);
        if (p is null) return null;
        return new PurchaseDetailsModel
        {
            MovieId = p.MovieId,
            Title = p.Movie.Title,
            PosterUrl = p.Movie.PosterUrl,
            PurchaseDate = p.PurchaseDateTime,
            Price = p.TotalPrice,
            PurchaseNumber = p.PurchaseNumber
        };
    }

    public async Task<PagedResultSet<MovieCardModel>> GetFavoritesAsync(
        int userId, int pageSize = 30, int page = 1)
    {
        var query = _db.Favorites                 
            .Where(f => f.UserId == userId)            
            .Select(f => new MovieCardModel
            {
                Id = f.MovieId,
                Title = f.Movie.Title,
                PosterUrl = f.Movie.PosterUrl
            });

        var totalCount = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();

        return new PagedResultSet<MovieCardModel>(page, pageSize, totalCount, data);
    }
}

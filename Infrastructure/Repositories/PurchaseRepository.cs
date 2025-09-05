using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(MovieShopDbContext db) : base(db) { }

    public async Task<IReadOnlyList<Purchase>> GetByUserAsync(int userId)
        => await _db.Purchases.Include(p => p.Movie).Where(p => p.UserId == userId).OrderByDescending(p => p.PurchaseDateTime).ToListAsync();

    public async Task<Purchase?> GetByUserAndMovieAsync(int userId, int movieId)
        => await _db.Purchases.Include(p => p.Movie).FirstOrDefaultAsync(p => p.UserId == userId && p.MovieId == movieId);

    public Task<bool> IsPurchasedAsync(int userId, int movieId) =>
        _db.Purchases.AsNoTracking()
           .AnyAsync(p => p.UserId == userId && p.MovieId == movieId);
}

using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly MovieShopDbContext _db;
    public ReportRepository(MovieShopDbContext db) => _db = db;

    public async Task<int> GetTotalUsersAsync() => await _db.Users.CountAsync();

    public async Task<IReadOnlyList<AdminTopMovieModel>> GetTopPurchasedMoviesAsync(DateTime? from = null, DateTime? to = null, int take = 100)
    {
        var q = _db.Purchases.AsQueryable();
        if (from.HasValue) q = q.Where(p => p.PurchaseDateTime >= from.Value);
        if (to.HasValue) q = q.Where(p => p.PurchaseDateTime <= to.Value);

        var result = await q
            .GroupBy(p => p.MovieId)
            .Select(g => new AdminTopMovieModel
            {
                MovieId = g.Key,
                Title = g.Select(x => x.Movie.Title).FirstOrDefault()!,
                TotalPurchases = g.Count()
            })
            .OrderByDescending(x => x.TotalPurchases)
            .ThenBy(x => x.Title)
            .Take(take)
            .ToListAsync();

        return result;
    }
}

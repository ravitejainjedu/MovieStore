using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly MovieShopDbContext _db;
        public AdminService(MovieShopDbContext db) => _db = db;

        public async Task<PagedResultSet<AdminTopMovieModel>> GetTopPurchasedMoviesAsync(
            DateTime? from, DateTime? to, int pageSize = 30, int page = 1)
        {
            var purchases = _db.Purchases.AsNoTracking();

            if (from.HasValue)
                purchases = purchases.Where(p => p.PurchaseDateTime >= from.Value.Date);

            if (to.HasValue)
            {
                // inclusive end-date
                var toExclusive = to.Value.Date.AddDays(1);
                purchases = purchases.Where(p => p.PurchaseDateTime < toExclusive);
            }

            // group by movie and count
            var grouped = purchases
                .GroupBy(p => p.MovieId)
                .Select(g => new { MovieId = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            var totalGroups = await grouped.CountAsync();

            var pageItems = await grouped
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // join to movies to get titles
            var movieIds = pageItems.Select(x => x.MovieId).ToList();

            var titles = await _db.Movies.AsNoTracking()
                .Where(m => movieIds.Contains(m.Id))
                .Select(m => new { m.Id, m.Title })
                .ToListAsync();

            var data = pageItems
                .Join(titles, x => x.MovieId, t => t.Id,
                    (x, t) => new AdminTopMovieModel
                    {
                        MovieId = t.Id,
                        Title = t.Title,
                        TotalPurchases = x.Count
                    })
                .ToList();

            return new PagedResultSet<AdminTopMovieModel>(page, pageSize, totalGroups, data);
        }

        public async Task<int> CreateMovieAsync(CreateMovieModel model)
        {
            var entity = new Movie
            {
                Title = model.Title,
                PosterUrl = model.PosterUrl,
                Overview = model.Overview,
                Price = model.Price,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "admin"
            };

            _db.Movies.Add(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }
    }
}

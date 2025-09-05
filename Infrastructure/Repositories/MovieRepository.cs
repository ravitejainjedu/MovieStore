using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : EfRepository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext db) : base(db) { }

    public async Task<IReadOnlyList<Movie>> GetHighestGrossingMoviesAsync(int count = 30)
    {
        return await _db.Movies.OrderByDescending(m => m.Revenue ?? 0).Take(count).ToListAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _db.Movies
            .Include(m => m.Trailers)
            .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Movie>> GetByGenreAsync(int genreId, int pageSize = 30, int page = 1)
    {
        return await (from mg in _db.MovieGenres.AsNoTracking()
                      where mg.GenreId == genreId
                      join m in _db.Movies.AsNoTracking() on mg.MovieId equals m.Id
                      orderby (m.Revenue ?? 0) descending
                      select m)
                     .Distinct()                              // avoid duplicates
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToListAsync();
    }

    public async Task<decimal?> GetPriceAsync(int movieId)
    {
        return await _db.Movies
            .Where(m => m.Id == movieId)
            .Select(m => (decimal?)m.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<(IReadOnlyList<Movie> Movies, int TotalCount)> SearchAsync(string query, int pageSize, int page)
    {
        query = (query ?? string.Empty).Trim();

        var q = _db.Movies.AsNoTracking()
            .Where(m => EF.Functions.Like(m.Title, $"%{query}%"));

        var total = await q.CountAsync();

        var items = await q
            .OrderBy(m => m.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

}

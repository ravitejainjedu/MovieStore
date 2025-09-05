using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository : EfRepository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext db) : base(db) { }

    public override async Task<Cast?> GetByIdAsync(int id)
    {
        return await _db.Casts
            .Include(c => c.MovieCasts).ThenInclude(mc => mc.Movie)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cast?> GetByIdWithMoviesAsync(int id) => await GetByIdAsync(id);
}

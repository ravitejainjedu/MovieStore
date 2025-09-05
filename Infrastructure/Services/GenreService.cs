using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GenreService : IGenreService
{
    private readonly MovieShopDbContext _db;
    public GenreService(MovieShopDbContext db) => _db = db;

    public async Task<IReadOnlyList<GenreModel>> GetAllAsync()
    {
        return await _db.Genres.OrderBy(g => g.Name)
            .Select(g => new GenreModel { Id = g.Id, Name = g.Name })
            .ToListAsync();
    }
}

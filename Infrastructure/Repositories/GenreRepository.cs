using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class GenreRepository : EfRepository<Genre>, IGenreRepository
{
    public GenreRepository(MovieShopDbContext db) : base(db) { }
}

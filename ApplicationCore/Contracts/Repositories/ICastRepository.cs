using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface ICastRepository : IRepository<Cast>
{
    Task<Cast?> GetByIdWithMoviesAsync(int id);
}

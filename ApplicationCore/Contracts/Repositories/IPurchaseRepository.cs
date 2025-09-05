using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IPurchaseRepository : IRepository<Purchase>
{
    Task<IReadOnlyList<Purchase>> GetByUserAsync(int userId);
    Task<Purchase?> GetByUserAndMovieAsync(int userId, int movieId);    
    Task<bool> IsPurchasedAsync(int userId, int movieId);
}

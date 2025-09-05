using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IReportRepository
{
    Task<int> GetTotalUsersAsync();
    Task<IReadOnlyList<AdminTopMovieModel>> GetTopPurchasedMoviesAsync(DateTime? from = null, DateTime? to = null, int take = 100);
}

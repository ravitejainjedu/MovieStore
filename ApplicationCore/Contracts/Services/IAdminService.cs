using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAdminService
{   

    Task<PagedResultSet<AdminTopMovieModel>> GetTopPurchasedMoviesAsync(
            DateTime? from, DateTime? to, int pageSize = 30, int page = 1);

    Task<int> CreateMovieAsync(CreateMovieModel model);

}

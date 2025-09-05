using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    Task<IReadOnlyList<MovieCardModel>> GetTopGrossingAsync(int count = 30);
    Task<MovieDetailsModel?> GetMovieDetailsAsync(int id);
    Task<IReadOnlyList<MovieCardModel>> GetTestMovieCardsAsync();
    Task<IEnumerable<MovieCardModel>> GetByGenreAsync(int genreId, int pageSize = 30, int page = 1);
    Task<decimal> GetPriceAsync(int movieId);
    Task<PagedResultSet<MovieCardModel>> SearchAsync(string query, int pageSize = 30, int page = 1);
}

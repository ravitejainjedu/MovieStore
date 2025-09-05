using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    Task<IReadOnlyList<Movie>> GetHighestGrossingMoviesAsync(int count = 30);
    Task<Movie?> GetMovieByIdAsync(int id); // with Includes
    Task<List<Movie>> GetByGenreAsync(int genreId, int pageSize = 30, int page = 1);
    Task<decimal?> GetPriceAsync(int movieId);
    Task<(IReadOnlyList<Movie> Movies, int TotalCount)> SearchAsync(string query, int pageSize, int page);
}

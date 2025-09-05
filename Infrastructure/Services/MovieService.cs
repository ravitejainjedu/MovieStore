using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    public MovieService(IMovieRepository movieRepository) => _movieRepository = movieRepository;

    public async Task<IReadOnlyList<MovieCardModel>> GetTopGrossingAsync(int count = 30)
    {
        var movies = await _movieRepository.GetHighestGrossingMoviesAsync(count);
        return movies.Select(m => new MovieCardModel { Id = m.Id, Title = m.Title, PosterUrl = m.PosterUrl }).ToList();
    }

    public async Task<MovieDetailsModel?> GetMovieDetailsAsync(int id)
    {
        var m = await _movieRepository.GetMovieByIdAsync(id);
        if (m is null) return null;

        var model = new MovieDetailsModel
        {
            Id = m.Id,
            Title = m.Title,
            Overview = m.Overview,
            PosterUrl = m.PosterUrl,
            Revenue = m.Revenue,
            RunTime = m.RunTime,
            ReleaseDate = m.ReleaseDate
        };
        model.Genres = m.MovieGenres.Select(g => g.Genre.Name).ToList();
        model.Trailers = m.Trailers.Select(t => (t.Name, t.TrailerUrl)).ToList();
        model.Casts = m.MovieCasts.Select(mc => (mc.CastId, mc.Cast.Name, (string?)mc.Character, mc.Cast.ProfilePath)).ToList();
        return model;
    }

    public Task<IReadOnlyList<MovieCardModel>> GetTestMovieCardsAsync()
    {
        var list = new List<MovieCardModel>
        {
            new MovieCardModel{ Id = 101, Title = "Test Movie A", PosterUrl = "https://via.placeholder.com/342x513?text=Test+A" },
            new MovieCardModel{ Id = 102, Title = "Test Movie B", PosterUrl = "https://via.placeholder.com/342x513?text=Test+B" },
            new MovieCardModel{ Id = 103, Title = "Test Movie C", PosterUrl = "https://via.placeholder.com/342x513?text=Test+C" },
        };
        return Task.FromResult<IReadOnlyList<MovieCardModel>>(list);
    }

    public async Task<IEnumerable<MovieCardModel>> GetByGenreAsync(int genreId, int pageSize = 30, int page = 1)
    {
        var movies = await _movieRepository.GetByGenreAsync(genreId, pageSize, page);
        return movies.Select(m => new MovieCardModel
        {
            Id = m.Id,
            Title = m.Title ?? string.Empty,
            PosterUrl = m.PosterUrl ?? string.Empty
        }).ToList();
    }

    public async Task<decimal> GetPriceAsync(int movieId)
    {
        var price = await _movieRepository.GetPriceAsync(movieId);
        return price ?? 9.99m;   // sensible fallback
    }

    public async Task<PagedResultSet<MovieCardModel>> SearchAsync(string query, int pageSize = 30, int page = 1)
    {
        var (movies, total) = await _movieRepository.SearchAsync(query, pageSize, page);

        var cards = movies.Select(m => new MovieCardModel
        {
            Id = m.Id,
            Title = m.Title,
            PosterUrl = m.PosterUrl
        }).ToList();

        return new PagedResultSet<MovieCardModel>(page, pageSize, total, cards);
    }

}

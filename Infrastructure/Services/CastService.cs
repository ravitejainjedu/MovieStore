using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;
    public CastService(ICastRepository castRepository) => _castRepository = castRepository;

    public async Task<CastDetailsModel?> GetCastDetailsAsync(int id)
    {
        var cast = await _castRepository.GetByIdWithMoviesAsync(id);
        if (cast is null) return null;

        var model = new CastDetailsModel
        {
            Id = cast.Id,
            Name = cast.Name,
            ProfilePath = cast.ProfilePath
        };
        model.Movies = cast.MovieCasts.Select(mc => (mc.MovieId, mc.Movie.Title, mc.Movie.PosterUrl, mc.Character)).ToList();
        return model;
    }
}

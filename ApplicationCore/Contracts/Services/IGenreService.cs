using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IGenreService
{
    Task<IReadOnlyList<GenreModel>> GetAllAsync();
}

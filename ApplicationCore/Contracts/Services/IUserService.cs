using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    Task<IReadOnlyList<PurchaseDetailsModel>> GetUserPurchasesAsync(int userId);
    Task<PurchaseDetailsModel?> GetPurchaseDetailsAsync(int userId, int movieId);
    Task<PagedResultSet<MovieCardModel>> GetFavoritesAsync(int userId, int pageSize = 30, int page = 1);
}

using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public Task<bool> IsMoviePurchasedAsync(int movieId, int userId) =>
            _purchaseRepository.IsPurchasedAsync(userId, movieId);

        public async Task PurchaseAsync(int movieId, int userId, decimal price)
        {
            // idempotent – don’t create duplicates
            if (await _purchaseRepository.IsPurchasedAsync(userId, movieId)) return;

            var purchase = new Purchase
            {
                MovieId = movieId,
                UserId = userId,
                TotalPrice = price,
                PurchaseNumber = Guid.NewGuid(),
                PurchaseDateTime = DateTime.UtcNow
            };

            await _purchaseRepository.AddAsync(purchase);
        }
    }
}

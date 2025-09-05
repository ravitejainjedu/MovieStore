using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IPurchaseService
    {
        Task<bool> IsMoviePurchasedAsync(int movieId, int userId);
        Task PurchaseAsync(int movieId, int userId, decimal price);
    }
}

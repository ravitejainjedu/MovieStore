using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task SaveChangesAsync();
        Task<IReadOnlyList<string>> GetRoleNamesForUserAsync(int userId);

    }
}

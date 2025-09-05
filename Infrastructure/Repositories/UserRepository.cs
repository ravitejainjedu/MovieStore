using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieShopDbContext _db;
        public UserRepository(MovieShopDbContext db) => _db = db;

        public async Task<User?> GetByEmailAsync(string email) =>
            await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
      
        public async Task<IReadOnlyList<string>> GetRoleNamesForUserAsync(int userId)
        {
            
            return await _db.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            
            // return await _db.UserRoles
            //    .Where(ur => ur.UserId == userId)
            //    .Join(_db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            //    .ToListAsync();
        }

        // --- IRepository<User> members you already implemented ---
        public async Task<User?> GetByIdAsync(int id) => await _db.Users.FindAsync(id);
        public async Task<User> AddAsync(User entity) { _db.Users.Add(entity); await _db.SaveChangesAsync(); return entity; }
        public async Task<User> UpdateAsync(User entity) { _db.Users.Update(entity); await _db.SaveChangesAsync(); return entity; }
        public async Task DeleteAsync(User entity) { _db.Users.Remove(entity); await _db.SaveChangesAsync(); }
        public async Task<IReadOnlyList<User>> ListAllAsync() => await _db.Users.AsNoTracking().ToListAsync();
        public async Task<IReadOnlyList<User>> ListAsync(System.Linq.Expressions.Expression<System.Func<User, bool>> predicate)
            => await _db.Users.AsNoTracking().Where(predicate).ToListAsync();
    }
}

using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _users;
        private readonly MovieShopDbContext _db;

        public AccountService(IUserRepository users, MovieShopDbContext db)
        {
            _users = users; 
            _db = db;
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterModel model)
        {
            // email already registered?
            var existing = await _users.GetByEmailAsync(model.Email);
            if (existing != null)
                return new AuthResult { Success = false, ErrorMessage = "Email already registered." };

            // salt + hash
            var saltBytes = RandomNumberGenerator.GetBytes(16);
            var salt = Convert.ToBase64String(saltBytes);

            using var hmac = new HMACSHA512(saltBytes);
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            var hashedPassword = Convert.ToBase64String(hashBytes);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                HashedPassword = hashedPassword,
                Salt = salt,
                IsLocked = false
            };

            await _users.AddAsync(user);
            // SaveChangesAsync is a no-op here because AddAsync saved,
            return new AuthResult { Success = true };
        }

        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            var user = await _users.GetByEmailAsync(model.Email);
            if (user == null)
                return new AuthResult { Success = false, ErrorMessage = "Invalid email or password." };

            // recompute hash with stored salt
            var saltBytes = Convert.FromBase64String(user.Salt);
            using var hmac = new HMACSHA512(saltBytes);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));            

            // roles
            var roles = await (from ur in _db.UserRoles
                               join r in _db.Roles on ur.RoleId equals r.Id
                               where ur.UserId == user.Id
                               select r.Name).ToListAsync();


            // compare bytes in constant time
            var stored = Convert.FromBase64String(user.HashedPassword);
            var ok = CryptographicOperations.FixedTimeEquals(stored, computed);

            if (!ok)
                return new AuthResult { Success = false, ErrorMessage = "Invalid email or password." };

            return new AuthResult {
                Success = true,
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                Roles = roles
            };
        }
    }
}

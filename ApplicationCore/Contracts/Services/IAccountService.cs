using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAccountService {
    Task<AuthResult> RegisterUserAsync(RegisterModel model);
    Task<AuthResult> LoginAsync(LoginModel model);
}

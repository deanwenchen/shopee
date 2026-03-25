namespace AdminAPI.Features.Auth.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(string username, string password);
    Task<bool> LogoutAsync(int adminId);
    Task<LoginResponse?> RefreshTokenAsync(string refreshToken);
}

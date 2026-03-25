namespace AdminAPI.Features.Auth.Services;

public interface IJwtService
{
    string GenerateToken(int adminId, string username, List<string> permissions);
    string GenerateRefreshToken();
    int? ValidateToken(string token);
}

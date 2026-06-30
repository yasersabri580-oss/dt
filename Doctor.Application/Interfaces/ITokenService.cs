namespace Doctor.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(long userId, string userName, string role);
    string GenerateRefreshToken();
    (long userId, string userName, string role) GetClaimsFromToken(string token);
}

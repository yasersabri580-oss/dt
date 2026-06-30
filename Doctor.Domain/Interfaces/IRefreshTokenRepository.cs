using Doctor.Domain.Entities;
namespace Doctor.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddAsync(RefreshToken token);
    void Delete(RefreshToken token);
    Task DeleteAllForUserAsync(long userId);

    /// <summary>Returns all non-revoked, non-expired sessions for a user.</summary>
    Task<List<RefreshToken>> GetActiveSessionsByUserIdAsync(long userId);

    /// <summary>Returns a specific session owned by the given user.</summary>
    Task<RefreshToken?> GetBySessionIdAsync(long userId, Guid sessionId);
}

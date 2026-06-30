
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _db;
    public RefreshTokenRepository(AppDbContext db) => _db = db;

    public async Task<RefreshToken?> GetByTokenAsync(string token) =>
        await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);

    public async Task AddAsync(RefreshToken token) =>
        await _db.RefreshTokens.AddAsync(token);

    public void Delete(RefreshToken token) =>
        _db.RefreshTokens.Remove(token);

    public async Task DeleteAllForUserAsync(long userId)
    {
        var tokens = await _db.RefreshTokens.Where(r => r.UserId == userId).ToListAsync();
        _db.RefreshTokens.RemoveRange(tokens);
    }

    public Task<List<RefreshToken>> GetActiveSessionsByUserIdAsync(long userId) =>
        _db.RefreshTokens
            .Where(r => r.UserId == userId && !r.IsRevoked && r.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(r => r.LastUsedAt ?? r.CreatedAt)
            .ToListAsync();

    public Task<RefreshToken?> GetBySessionIdAsync(long userId, Guid sessionId) =>
        _db.RefreshTokens
            .FirstOrDefaultAsync(r => r.UserId == userId && r.SessionId == sessionId && !r.IsRevoked);
}

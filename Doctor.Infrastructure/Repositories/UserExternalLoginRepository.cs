
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class UserExternalLoginRepository : IUserExternalLoginRepository
{
    private readonly AppDbContext _db;

    public UserExternalLoginRepository(AppDbContext db) => _db = db;

    public Task<UserExternalLogin?> GetAsync(string provider, string providerUserId) =>
        _db.UserExternalLogins
            .FirstOrDefaultAsync(e =>
                e.Provider == provider &&
                e.ProviderUserId == providerUserId);

    public Task<List<UserExternalLogin>> GetByUserIdAsync(long userId) =>
        _db.UserExternalLogins.Where(e => e.UserId == userId).ToListAsync();

    public async Task AddAsync(UserExternalLogin login) =>
        await _db.UserExternalLogins.AddAsync(login);
}

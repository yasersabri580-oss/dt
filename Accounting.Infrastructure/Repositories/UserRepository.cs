using Accounting.Domain.Entities;
using Accounting.Domain.Interfaces;
using Accounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _db.Users.AsNoTracking().ToListAsync();

    public async Task<User?> GetByIdAsync(long id) =>
        await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.IdUser == id);

    public async Task<User?> GetByUserNameAsync(string userName) =>
        await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName);

    public async Task<bool> AdminExistsAsync() =>
        await _db.Users.AnyAsync(u => u.Role == "Admin");

    public async Task<bool> AnyUserExistsAsync() =>
        await _db.Users.AnyAsync();

    public async Task AddAsync(User user) =>
        await _db.Users.AddAsync(user);

    public void Update(User user) =>
        _db.Users.Update(user);

    public void Delete(User user) =>
        _db.Users.Remove(user);
}

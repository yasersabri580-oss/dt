// StatRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class StatRepository : IStatRepository
{
    private readonly AppDbContext _db;
    public StatRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Stat>> GetAllAsync() =>
        await _db.Stats.AsNoTracking().ToListAsync();

    public async Task<Stat?> GetByIdAsync(long id) =>
        await _db.Stats.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Stat?> GetByUserIdAsync(long userId) =>
        await _db.Stats
            .AsNoTracking()
            .Join(_db.Doctors, s => s.DoctorId, d => d.Id, (s, d) => new { s, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.s)
            .FirstOrDefaultAsync();

    public async Task<Stat?> GetByDoctorIdAsync(Guid id) =>
        await _db.Stats.FirstOrDefaultAsync(s => s.DoctorId == id);

    public async Task AddAsync(Stat stat) =>
        await _db.Stats.AddAsync(stat);

    public void Update(Stat stat) =>
        _db.Stats.Update(stat);

    public void Delete(Stat stat) =>
        _db.Stats.Remove(stat);
}
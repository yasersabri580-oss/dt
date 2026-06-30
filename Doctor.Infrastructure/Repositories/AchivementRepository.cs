// AchivementRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class AchievementRepository : IAchivementRepository
{
    private readonly AppDbContext _db;
    public AchievementRepository(AppDbContext db) => _db = db;

    public Task<List<Achievement>> GetAllAsync() =>
        _db.Achievements.AsNoTracking().ToListAsync();

    public Task<Achievement?> GetByIdAsync(long id) =>
        _db.Achievements.FirstOrDefaultAsync(a => a.Id == id);

    public Task<Achievement?> GetByUserIdAsync(long userId) =>
        _db.Achievements
            .AsNoTracking()
            .Join(_db.Doctors,
                a => a.DoctorId, d => d.Id,
                (a, d) => new { a, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.a)
            .FirstOrDefaultAsync();

    public async Task AddAsync(Achievement achievement) =>
        await _db.Achievements.AddAsync(achievement);

    public void Update(Achievement achievement) =>
        _db.Achievements.Update(achievement);

    public void Delete(Achievement achievement) =>
        _db.Achievements.Remove(achievement);

    public Task<Achievement?> GetByDoctorIdAsync(Guid id) =>
        _db.Achievements.FirstOrDefaultAsync(a => a.DoctorId == id);
}
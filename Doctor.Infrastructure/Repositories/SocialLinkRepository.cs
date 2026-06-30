// SocialLinkRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class SocialLinkRepository : ISocialLinkRepository
{
    private readonly AppDbContext _db;
    public SocialLinkRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<SocialLink>> GetAllAsync() =>
        await _db.SocialLinks.AsNoTracking().ToListAsync();

    public async Task<SocialLink?> GetByIdAsync(long id) =>
        await _db.SocialLinks.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<SocialLink?> GetByUserIdAsync(long userId) =>
        await _db.SocialLinks
            .AsNoTracking()
            .Join(_db.Doctors, s => s.DoctorId, d => d.Id, (s, d) => new { s, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.s)
            .FirstOrDefaultAsync();

    public async Task<SocialLink?> GetByDoctorIdAsync(Guid id) =>
        await _db.SocialLinks.FirstOrDefaultAsync(s => s.DoctorId == id);

    public async Task AddAsync(SocialLink socialLink) =>
        await _db.SocialLinks.AddAsync(socialLink);

    public void Update(SocialLink socialLink) =>
        _db.SocialLinks.Update(socialLink);

    public void Delete(SocialLink socialLink) =>
        _db.SocialLinks.Remove(socialLink);
}
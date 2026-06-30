// HeroImageRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class HeroImageRepository : IHeroImageRepository
{
    private readonly AppDbContext _db;
    public HeroImageRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<HeroImage>> GetAllAsync() =>
        await _db.HeroImages.AsNoTracking().ToListAsync();

    public async Task<HeroImage?> GetByIdAsync(long id) =>
        await _db.HeroImages.FirstOrDefaultAsync(h => h.Id == id);

    public async Task<HeroImage?> GetByUserIdAsync(long userId) =>
        await _db.HeroImages
            .AsNoTracking()
            .Join(_db.Doctors, h => h.DoctorId, d => d.Id, (h, d) => new { h, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.h)
            .FirstOrDefaultAsync();

    public async Task<HeroImage?> GetByDoctorIdAsync(Guid doctorId) =>
        await _db.HeroImages.FirstOrDefaultAsync(h => h.DoctorId == doctorId);

    public async Task AddAsync(HeroImage heroImage) =>
        await _db.HeroImages.AddAsync(heroImage);

    public void Update(HeroImage heroImage) =>
        _db.HeroImages.Update(heroImage);

    public void Delete(HeroImage heroImage) =>
        _db.HeroImages.Remove(heroImage);
}
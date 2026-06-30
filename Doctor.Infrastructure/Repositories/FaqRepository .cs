// FaqRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class FaqRepository : IFaqRepository
{
    private readonly AppDbContext _db;
    public FaqRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Faq>> GetAllAsync() =>
        await _db.Faqs.AsNoTracking().ToListAsync();

    public async Task<Faq?> GetByIdAsync(long id) =>
        await _db.Faqs.FirstOrDefaultAsync(f => f.Id == id);

    public async Task<Faq?> GetByUserIdAsync(long userId) =>
        await _db.Faqs
            .AsNoTracking()
            .Join(_db.Doctors, f => f.DoctorId, d => d.Id, (f, d) => new { f, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.f)
            .FirstOrDefaultAsync();

    public async Task<bool> ExistsForUserAsync(long userId) =>
        await _db.Faqs
            .Join(_db.Doctors, f => f.DoctorId, d => d.Id, (f, d) => new { f, d.UserId })
            .AnyAsync(x => x.UserId == userId);

    public async Task<IEnumerable<Faq>> GetByDoctorIdAsync(Guid doctorId) =>
        await _db.Faqs.AsNoTracking().Where(f => f.DoctorId == doctorId).ToListAsync();

    public async Task AddAsync(Faq faq) =>
        await _db.Faqs.AddAsync(faq);

    public void Update(Faq faq) =>
        _db.Faqs.Update(faq);

    public void Delete(Faq faq) =>
        _db.Faqs.Remove(faq);
}
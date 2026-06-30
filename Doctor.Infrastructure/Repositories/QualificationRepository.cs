// QualificationRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class QualificationRepository : IQualificationRepository
{
    private readonly AppDbContext _db;
    public QualificationRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Qualification>> GetAllAsync() =>
        await _db.Qualifications.AsNoTracking().ToListAsync();

    public async Task<Qualification?> GetByIdAsync(long id) =>
        await _db.Qualifications.FirstOrDefaultAsync(q => q.Id == id);

    public async Task<Qualification?> GetByUserIdAsync(long userId) =>
        await _db.Qualifications
            .AsNoTracking()
            .Join(_db.Doctors, q => q.DoctorId, d => d.Id, (q, d) => new { q, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.q)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Qualification>> GetByDoctorIdAsync(Guid doctorId) =>
        await _db.Qualifications.AsNoTracking().Where(q => q.DoctorId == doctorId).ToListAsync();

    public async Task AddAsync(Qualification qualification) =>
        await _db.Qualifications.AddAsync(qualification);

    public void Update(Qualification qualification) =>
        _db.Qualifications.Update(qualification);

    public void Delete(Qualification qualification) =>
        _db.Qualifications.Remove(qualification);
}
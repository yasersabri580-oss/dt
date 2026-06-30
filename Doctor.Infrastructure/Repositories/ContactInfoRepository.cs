// ContactInfoRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class ContactInfoRepository : IContactInfoRepository
{
    private readonly AppDbContext _db;
    public ContactInfoRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<ContactInfo>> GetAllAsync() =>
        await _db.ContactInfos.AsNoTracking().ToListAsync();

    public async Task<ContactInfo?> GetByIdAsync(long id) =>
        await _db.ContactInfos.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<ContactInfo?> GetByUserIdAsync(long userId) =>
        await _db.ContactInfos
            .AsNoTracking()
            .Join(_db.Doctors, c => c.DoctorId, d => d.Id, (c, d) => new { c, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.c)
            .FirstOrDefaultAsync();

    public async Task<ContactInfo?> GetByDoctorIdAsync(Guid id) =>
        await _db.ContactInfos.FirstOrDefaultAsync(c => c.DoctorId == id);

    public async Task AddAsync(ContactInfo contactInfo) =>
        await _db.ContactInfos.AddAsync(contactInfo);

    public void Update(ContactInfo contactInfo) =>
        _db.ContactInfos.Update(contactInfo);

    public void Delete(ContactInfo contactInfo) =>
        _db.ContactInfos.Remove(contactInfo);
}
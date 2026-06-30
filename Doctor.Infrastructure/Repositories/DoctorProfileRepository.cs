using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class DoctorProfileRepository : IDoctorProfileRepository
{
    private readonly AppDbContext _db;
    public DoctorProfileRepository(AppDbContext db) => _db = db;

    public async Task<DoctorProfile?> GetByIdAsync(long id) =>
        await _db.DoctorProfiles.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<DoctorProfile?> GetByDoctorIdAsync(Guid doctorId) =>
        await _db.DoctorProfiles.FirstOrDefaultAsync(p => p.DoctorId == doctorId);

    public async Task<bool> ExistsForDoctorAsync(Guid doctorId) =>
        await _db.DoctorProfiles.AnyAsync(p => p.DoctorId == doctorId);

    public async Task AddAsync(DoctorProfile profile) =>
        await _db.DoctorProfiles.AddAsync(profile);

    public void Update(DoctorProfile profile) =>
        _db.DoctorProfiles.Update(profile);

    public void Delete(DoctorProfile profile) =>
        _db.DoctorProfiles.Remove(profile);

    public async Task<DoctorProfile?> GetByUserIdAsync(long userId) =>
        await _db.DoctorProfiles
            .AsNoTracking()
            .Join(
                _db.Doctors,
                profile => profile.DoctorId,
                doctor => doctor.Id,
                (profile, doctor) => new { profile, doctor.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.profile)
            .FirstOrDefaultAsync();
}
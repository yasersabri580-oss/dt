using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _db;
    public DoctorRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Domain.Entities.Doctor>> GetAllAsync() =>
        await _db.Doctors.AsNoTracking().ToListAsync();

    public async Task<Domain.Entities.Doctor?> GetByIdAsync(Guid id) =>
        await _db.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Domain.Entities.Doctor?> GetByIdWithProfileAsync(Guid id) =>
        await _db.Doctors
            .AsNoTracking()
            .Include(d => d.Profile)
            .Include(d => d.ContactInfo)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Domain.Entities.Doctor?> GetByUserIdAsync(long userId) =>
        await _db.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);

    public async Task<Domain.Entities.Doctor?> GetByUserIdWithProfileAsync(long userId) =>
        await _db.Doctors
            .AsNoTracking()
            .Include(d => d.Profile)
            .Include(d => d.ContactInfo)
            .FirstOrDefaultAsync(d => d.UserId == userId);

    public async Task<bool> ExistsForUserAsync(long userId) =>
        await _db.Doctors.AnyAsync(d => d.UserId == userId);

    public async Task AddAsync(Domain.Entities.Doctor doctor) =>
        await _db.Doctors.AddAsync(doctor);

    public void Update(Domain.Entities.Doctor doctor) =>
        _db.Doctors.Update(doctor);

    public void Delete(Domain.Entities.Doctor doctor) =>
        _db.Doctors.Remove(doctor);

    public Task<IEnumerable<Domain.Entities.Doctor>> GetDoctorsBySpecializationAsync(string specialization)
    {
        throw new NotImplementedException();
    }
}
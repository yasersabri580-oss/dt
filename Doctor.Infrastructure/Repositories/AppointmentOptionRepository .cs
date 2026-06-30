// AppointmentOptionRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class AppointmentOptionRepository : IAppointmentOptionRepository
{
    private readonly AppDbContext _db;
    public AppointmentOptionRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<AppointmentOption>> GetAllAsync() =>
        await _db.AppointmentOptions.AsNoTracking().ToListAsync();

    public async Task<AppointmentOption?> GetByIdAsync(long id) =>
        await _db.AppointmentOptions.FirstOrDefaultAsync(a => a.Id == id);

    public async Task<AppointmentOption?> GetByUserIdAsync(long userId) =>
        await _db.AppointmentOptions
            .AsNoTracking()
            .Join(_db.Doctors, a => a.DoctorId, d => d.Id, (a, d) => new { a, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.a)
            .FirstOrDefaultAsync();

    public async Task<AppointmentOption?> GetByDoctorIdAsync(Guid doctorId) =>
        await _db.AppointmentOptions.FirstOrDefaultAsync(a => a.DoctorId == doctorId);

    public async Task AddAsync(AppointmentOption appointmentOption) =>
        await _db.AppointmentOptions.AddAsync(appointmentOption);

    public void Update(AppointmentOption appointmentOption) =>
        _db.AppointmentOptions.Update(appointmentOption);

    public void Delete(AppointmentOption appointmentOption) =>
        _db.AppointmentOptions.Remove(appointmentOption);
}
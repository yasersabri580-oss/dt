// DoctorServiceRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class DoctorServiceRepository : IDoctorServiceRepository
{
    private readonly AppDbContext _db;
    public DoctorServiceRepository(AppDbContext db) => _db = db;

    public async Task<DoctorService?> GetByIdAsync(long id) =>
        await _db.DoctorServices.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<DoctorService?> GetByDoctorIdAsync(Guid doctorId) =>
        await _db.DoctorServices.FirstOrDefaultAsync(s => s.DoctorId == doctorId);

    public async Task<bool> ExistsForDoctorAsync(Guid doctorId) =>
        await _db.DoctorServices.AnyAsync(s => s.DoctorId == doctorId);

    public async Task AddAsync(DoctorService profile) =>
        await _db.DoctorServices.AddAsync(profile);

    public void Update(DoctorService profile) =>
        _db.DoctorServices.Update(profile);

    public void Delete(DoctorService profile) =>
        _db.DoctorServices.Remove(profile);
}
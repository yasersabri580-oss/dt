// ReviewRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _db;
    public ReviewRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Review>> GetAllAsync() =>
        await _db.Reviews.AsNoTracking().ToListAsync();

    public async Task<Review?> GetByIdAsync(long id) =>
        await _db.Reviews.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<Review?> GetByUserIdAsync(long userId) =>
        await _db.Reviews
            .AsNoTracking()
            .Join(_db.Doctors, r => r.DoctorId, d => d.Id, (r, d) => new { r, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.r)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Review>> GetByDoctorIdAsync(Guid doctorId) =>
        await _db.Reviews.AsNoTracking().Where(r => r.DoctorId == doctorId).ToListAsync();

    public async Task AddAsync(Review review) =>
        await _db.Reviews.AddAsync(review);

    public void Update(Review review) =>
        _db.Reviews.Update(review);

    public void Delete(Review review) =>
        _db.Reviews.Remove(review);
}
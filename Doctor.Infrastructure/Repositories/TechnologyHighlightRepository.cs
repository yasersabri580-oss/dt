// TechnologyHighlightRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class TechnologyHighlightRepository : ITechnologyHighlightRepository
{
    private readonly AppDbContext _db;
    public TechnologyHighlightRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<TechnologyHighlight>> GetAllAsync() =>
        await _db.TechnologyHighlights.AsNoTracking().ToListAsync();

    public async Task<TechnologyHighlight?> GetByIdAsync(long id) =>
        await _db.TechnologyHighlights.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TechnologyHighlight?> GetByUserIdAsync(long userId) =>
        await _db.TechnologyHighlights
            .AsNoTracking()
            .Join(_db.Doctors, t => t.DoctorId, d => d.Id, (t, d) => new { t, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.t)
            .FirstOrDefaultAsync();

    public async Task<TechnologyHighlight?> GetByDoctorIdAsync(Guid id) =>
        await _db.TechnologyHighlights.FirstOrDefaultAsync(t => t.DoctorId == id);

    public async Task AddAsync(TechnologyHighlight technologyHighlight) =>
        await _db.TechnologyHighlights.AddAsync(technologyHighlight);

    public void Update(TechnologyHighlight technologyHighlight) =>
        _db.TechnologyHighlights.Update(technologyHighlight);

    public void Delete(TechnologyHighlight technologyHighlight) =>
        _db.TechnologyHighlights.Remove(technologyHighlight);
}
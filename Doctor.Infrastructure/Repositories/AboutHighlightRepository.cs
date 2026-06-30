// AboutHighlightRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class AboutHighlightRepository : IAboutHighlightRepository
{
    private readonly AppDbContext _context;
    public AboutHighlightRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<AboutHighlight>> GetAllAsync() =>
        await _context.AboutHighlights.AsNoTracking().ToListAsync();

    public async Task<AboutHighlight?> GetByIdAsync(long id) =>
        await _context.AboutHighlights.FirstOrDefaultAsync(a => a.Id == id);

    public async Task<AboutHighlight?> GetByUserIdAsync(long userId) =>
        await _context.AboutHighlights
            .AsNoTracking()
            .Join(_context.Doctors,
                h => h.DoctorId, d => d.Id,
                (h, d) => new { h, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.h)
            .FirstOrDefaultAsync();

    public async Task<AboutHighlight?> GetByDoctorIdAsync(Guid id) =>
        await _context.AboutHighlights.FirstOrDefaultAsync(a => a.DoctorId == id);

    public async Task AddAsync(AboutHighlight aboutHighlight) =>
        await _context.AboutHighlights.AddAsync(aboutHighlight);

    public void Update(AboutHighlight aboutHighlight) =>
        _context.AboutHighlights.Update(aboutHighlight);

    public void Delete(AboutHighlight aboutHighlight) =>
        _context.AboutHighlights.Remove(aboutHighlight);
}
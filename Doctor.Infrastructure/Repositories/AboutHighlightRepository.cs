

using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class AboutHighlightRepository : IAboutHighlightRepository
{
      private readonly AppDbContext _context;
    public AboutHighlightRepository(AppDbContext context) => _context = context;


    public async Task<IEnumerable<AboutHighlight>> GetAllAsync()
    {
        return await _context.AboutHighlights.ToListAsync();
    }

    public async Task<AboutHighlight?> GetByIdAsync(Guid id)
    {
        return await _context.AboutHighlights.FindAsync(id);
    }

    public async Task<AboutHighlight?> GetByUserIdAsync(long userId)
    {
        return null;
    }

    public async Task AddAsync(AboutHighlight aboutHighlight)
    {
        await _context.AboutHighlights.AddAsync(aboutHighlight);
        await _context.SaveChangesAsync();
    }

    public void Update(AboutHighlight aboutHighlight)
    {
        _context.AboutHighlights.Update(aboutHighlight);
        _context.SaveChanges();
    }

    public void Delete(AboutHighlight aboutHighlight)
    {
        _context.AboutHighlights.Remove(aboutHighlight);
        _context.SaveChanges();
    }

    Task<IEnumerable<AboutHighlight>> IAboutHighlightRepository.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<AboutHighlight> IAboutHighlightRepository.GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<AboutHighlight> IAboutHighlightRepository.GetByUserIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<AboutHighlight?> GetByDoctorIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
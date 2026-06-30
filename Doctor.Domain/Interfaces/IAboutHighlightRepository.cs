// IAboutHighlightRepository.cs
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IAboutHighlightRepository
{
    Task<IEnumerable<AboutHighlight>> GetAllAsync();
    Task<AboutHighlight?> GetByIdAsync(long id);
    Task<AboutHighlight?> GetByUserIdAsync(long userId);
    Task<AboutHighlight?> GetByDoctorIdAsync(Guid id);
    Task AddAsync(AboutHighlight aboutHighlight);
    void Update(AboutHighlight aboutHighlight);
    void Delete(AboutHighlight aboutHighlight);
}
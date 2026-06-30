

using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;


public interface IAboutHighlightRepository
{
    Task<IEnumerable<AboutHighlight>> GetAllAsync();
    Task<AboutHighlight> GetByIdAsync(Guid id);
    Task<AboutHighlight> GetByUserIdAsync(long userId);
    Task AddAsync(AboutHighlight aboutHighlight);
    void Update(AboutHighlight aboutHighlight);
    void Delete(AboutHighlight aboutHighlight);

    Task<AboutHighlight?> GetByDoctorIdAsync(Guid id);
}
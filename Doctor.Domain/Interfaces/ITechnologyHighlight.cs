
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface ITechnologyHighlightRepository
{
    Task<IEnumerable<TechnologyHighlight>> GetAllAsync();
    Task<TechnologyHighlight> GetByIdAsync(Guid id);
    Task<TechnologyHighlight> GetByUserIdAsync(long userId);
    Task AddAsync(TechnologyHighlight technologyHighlight);
    void Update(TechnologyHighlight technologyHighlight);
    void Delete(TechnologyHighlight technologyHighlight);
}
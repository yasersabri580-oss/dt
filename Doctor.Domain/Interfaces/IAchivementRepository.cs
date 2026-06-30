
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IAchivementRepository
{
    Task<IEnumerable<Achievement>> GetAllAsync();
    Task<Achievement> GetByIdAsync(Guid id);
    Task<Achievement> GetByUserIdAsync(long userId);
    Task AddAsync(Achievement achievement);
    void Update(Achievement achievement);
    void Delete(Achievement achievement);
}

using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IAchivementRepository
{
    Task<List<Achievement>> GetAllAsync();
    Task<Achievement?> GetByIdAsync(long id);
    Task<Achievement?> GetByUserIdAsync(long userId);
    Task AddAsync(Achievement achievement);
    void Update(Achievement achievement);
    void Delete(Achievement achievement);

    Task<Achievement?> GetByDoctorIdAsync(Guid id);
}
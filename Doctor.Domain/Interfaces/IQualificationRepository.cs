
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;
public interface IQualificationRepository
{
    Task<IEnumerable<Qualification>> GetAllAsync();
    Task<Qualification> GetByIdAsync(Guid id);
    Task<Qualification> GetByUserIdAsync(long userId);
    Task AddAsync(Qualification qualification);
    void Update(Qualification qualification);
    void Delete(Qualification qualification);
}
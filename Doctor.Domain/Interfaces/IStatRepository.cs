// IStatRepository.cs
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IStatRepository
{
    Task<IEnumerable<Stat>> GetAllAsync();
    Task<Stat?> GetByIdAsync(long id);
    Task<Stat?> GetByUserIdAsync(long userId);
    Task AddAsync(Stat stat);
    void Update(Stat stat);
    void Delete(Stat stat);
    Task<Stat?> GetByDoctorIdAsync(Guid id);
}
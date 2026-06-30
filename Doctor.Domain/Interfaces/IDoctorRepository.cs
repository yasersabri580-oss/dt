using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor.Domain.Entities.Doctor>> GetAllAsync();
    Task<Entities.Doctor?> GetByIdAsync(Guid id);
    Task<Entities.Doctor?> GetByIdWithProfileAsync(Guid id);
    Task<Entities.Doctor?> GetByUserIdAsync(long userId);
    Task<Entities.Doctor?> GetByUserIdWithProfileAsync(long userId);
    Task<bool> ExistsForUserAsync(long userId);
    Task AddAsync(Entities.Doctor doctor);
    void Update(Entities.Doctor doctor);
    void Delete(Entities.Doctor doctor);
}
// IQualificationRepository.cs
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IQualificationRepository
{
    Task<IEnumerable<Qualification>> GetAllAsync();
    Task<Qualification?> GetByIdAsync(long id);
    Task<Qualification?> GetByUserIdAsync(long userId);
    Task AddAsync(Qualification qualification);
    void Update(Qualification qualification);
    void Delete(Qualification qualification);
    Task<IEnumerable<Qualification>> GetByDoctorIdAsync(Guid doctorId);
}
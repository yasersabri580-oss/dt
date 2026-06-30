using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IDoctorServiceRepository
{
    Task<DoctorService?> GetByIdAsync(long id);
    Task<DoctorService?> GetByDoctorIdAsync(Guid doctorId);
    Task<bool> ExistsForDoctorAsync(Guid doctorId);
    Task AddAsync(DoctorService profile);
    void Update(DoctorService profile);
    void Delete(DoctorService profile);
}
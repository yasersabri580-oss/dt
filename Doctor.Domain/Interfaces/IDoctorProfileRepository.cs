using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IDoctorProfileRepository
{
    Task<DoctorProfile?> GetByIdAsync(long id);
    Task<DoctorProfile?> GetByDoctorIdAsync(Guid doctorId);
    Task<bool> ExistsForDoctorAsync(Guid doctorId);
    Task AddAsync(DoctorProfile profile);
    void Update(DoctorProfile profile);
    void Delete(DoctorProfile profile);
}
using Doctor.Application.DTOs.Doctor;

namespace Doctor.Application.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAllAsync();
    Task<DoctorDto?> GetByIdAsync(Guid id);
    Task<DoctorWithProfileDto?> GetByIdWithProfileAsync(Guid id);
    Task<DoctorWithProfileDto?> GetByUserIdWithProfileAsync(long userId);

    /// <summary>Create a doctor record for an existing user account.</summary>
    Task<DoctorDto> CreateAsync(CreateDoctorDto dto);

    Task<DoctorDto?> UpdateAsync(Guid id, UpdateDoctorDto dto);
    Task<bool> DeleteAsync(Guid id);
}
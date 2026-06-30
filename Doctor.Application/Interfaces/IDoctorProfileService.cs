using Doctor.Application.DTOs.DoctorProfile;

namespace Doctor.Application.Interfaces;

public interface IDoctorProfileService
{
    Task<DoctorProfileDto?> GetByDoctorIdAsync(Guid doctorId);

    /// <summary>Creates the profile for a doctor. Fails if a profile already exists.</summary>
    Task<DoctorProfileDto> CreateAsync(CreateDoctorProfileDto dto);

    Task<DoctorProfileDto?> UpdateAsync(Guid doctorId, UpdateDoctorProfileDto dto);
    Task<bool> DeleteAsync(Guid doctorId);
}
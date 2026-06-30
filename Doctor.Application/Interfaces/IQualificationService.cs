using Doctor.Application.DTOs.Qualification;

namespace Doctor.Application.Interfaces;

public interface IQualificationService
{
    Task<IEnumerable<QualificationDto>> GetAllAsync();
    Task<QualificationDto?> GetByIdAsync(long id);
    Task<QualificationDto?> GetByUserIdAsync(long userId);
    Task<IEnumerable<QualificationDto>> GetByDoctorIdAsync(Guid doctorId);
    Task<QualificationDto> CreateAsync(CreateQualificationDto dto);
    Task UpdateAsync(long id, UpdateQualificationDto dto);
    Task DeleteAsync(long id);
}

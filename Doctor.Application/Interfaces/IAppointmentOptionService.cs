using Doctor.Application.DTOs.AppointmentOption;

namespace Doctor.Application.Interfaces;

public interface IAppointmentOptionService
{
    Task<IEnumerable<AppointmentOptionDto>> GetAllAsync();
    Task<AppointmentOptionDto?> GetByIdAsync(long id);
    Task<AppointmentOptionDto?> GetByUserIdAsync(long userId);
    Task<AppointmentOptionDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<AppointmentOptionDto> CreateAsync(CreateAppointmentOptionDto dto);
    Task UpdateAsync(long id, UpdateAppointmentOptionDto dto);
    Task DeleteAsync(long id);
}

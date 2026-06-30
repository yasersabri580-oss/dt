using Doctor.Application.DTOs.DoctorService;

namespace Doctor.Application.Interfaces;

// Named "DoctorOffering" to avoid clashing with the generic concept of an
// application "service" interface — rename to taste (e.g. IDoctorServiceService).
public interface IDoctorOfferingService
{
    Task<DoctorServiceDto?> GetByIdAsync(long id);
    Task<DoctorServiceDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<bool> ExistsForDoctorAsync(Guid doctorId);
    Task<DoctorServiceDto> CreateAsync(CreateDoctorServiceDto dto);
    Task UpdateAsync(long id, UpdateDoctorServiceDto dto);
    Task DeleteAsync(long id);
}

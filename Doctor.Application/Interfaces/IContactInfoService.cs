using Doctor.Application.DTOs.ContactInfo;

namespace Doctor.Application.Interfaces;

public interface IContactInfoService
{
    Task<IEnumerable<ContactInfoDto>> GetAllAsync();
    Task<ContactInfoDto?> GetByIdAsync(long id);
    Task<ContactInfoDto?> GetByUserIdAsync(long userId);
    Task<ContactInfoDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<ContactInfoDto> CreateAsync(CreateContactInfoDto dto);
    Task UpdateAsync(long id, UpdateContactInfoDto dto);
    Task DeleteAsync(long id);
}

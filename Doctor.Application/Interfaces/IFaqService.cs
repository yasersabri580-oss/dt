using Doctor.Application.DTOs.Faq;

namespace Doctor.Application.Interfaces;

public interface IFaqService
{
    Task<IEnumerable<FaqDto>> GetAllAsync();
    Task<FaqDto?> GetByIdAsync(long id);
    Task<FaqDto?> GetByUserIdAsync(long userId);
    Task<bool> ExistsForUserAsync(long userId);
    Task<IEnumerable<FaqDto>> GetByDoctorIdAsync(Guid doctorId);
    Task<FaqDto> CreateAsync(CreateFaqDto dto);
    Task UpdateAsync(long id, UpdateFaqDto dto);
    Task DeleteAsync(long id);
}

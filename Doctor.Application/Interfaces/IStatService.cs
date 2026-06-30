using Doctor.Application.DTOs.Stat;

namespace Doctor.Application.Interfaces;

public interface IStatService
{
    Task<IEnumerable<StatDto>> GetAllAsync();
    Task<StatDto?> GetByIdAsync(long id);
    Task<StatDto?> GetByUserIdAsync(long userId);
    Task<StatDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<StatDto> CreateAsync(CreateStatDto dto);
    Task UpdateAsync(long id, UpdateStatDto dto);
    Task DeleteAsync(long id);
}

using Doctor.Application.DTOs.Review;

namespace Doctor.Application.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<ReviewDto?> GetByIdAsync(long id);
    Task<ReviewDto?> GetByUserIdAsync(long userId);
    Task<IEnumerable<ReviewDto>> GetByDoctorIdAsync(Guid doctorId);
    Task<ReviewDto> CreateAsync(CreateReviewDto dto);
    Task UpdateAsync(long id, UpdateReviewDto dto);
    Task DeleteAsync(long id);
}

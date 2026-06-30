
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review> GetByIdAsync(Guid id);
    Task<Review> GetByUserIdAsync(long userId);
    Task AddAsync(Review review);
    void Update(Review review);
    void Delete(Review review);
}
// IFaqRepository.cs
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IFaqRepository
{
    Task<IEnumerable<Faq>> GetAllAsync();
    Task<Faq?> GetByIdAsync(long id);
    Task<Faq?> GetByUserIdAsync(long userId);
    Task<bool> ExistsForUserAsync(long userId);
    Task AddAsync(Faq faq);
    void Update(Faq faq);
    void Delete(Faq faq);
    Task<IEnumerable<Faq>> GetByDoctorIdAsync(Guid doctorId);
}
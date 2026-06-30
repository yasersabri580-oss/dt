using Doctor.Domain.Entities;
namespace Doctor.Domain.Interfaces;
public interface IFaqRepository
    {
        Task<IEnumerable<Faq>> GetAllAsync();
        Task<Faq> GetByIdAsync(Guid id);
      
        Task<Faq> GetByUserIdAsync(long userId);
       
        Task<bool> ExistsForUserAsync(long userId);
        Task AddAsync(Faq faq);
        void Update(Faq faq);
        void Delete(Faq faq);
    }


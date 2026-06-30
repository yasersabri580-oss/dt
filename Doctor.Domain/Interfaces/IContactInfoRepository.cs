// IContactInfoRepository.cs
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IContactInfoRepository
{
    Task<IEnumerable<ContactInfo>> GetAllAsync();
    Task<ContactInfo?> GetByIdAsync(long id);
    Task<ContactInfo?> GetByUserIdAsync(long userId);
    Task AddAsync(ContactInfo contactInfo);
    void Update(ContactInfo contactInfo);
    void Delete(ContactInfo contactInfo);
    Task<ContactInfo?> GetByDoctorIdAsync(Guid id);
}
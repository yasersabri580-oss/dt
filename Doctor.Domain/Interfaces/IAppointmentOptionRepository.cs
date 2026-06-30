

using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IAppointmentOptionRepository
{
    Task<IEnumerable<AppointmentOption>> GetAllAsync();
    Task<AppointmentOption> GetByIdAsync(Guid id);
    Task<AppointmentOption> GetByUserIdAsync(long userId);
    Task AddAsync(AppointmentOption appointmentOption);
    void Update(AppointmentOption appointmentOption);
    void Delete(AppointmentOption appointmentOption);
}
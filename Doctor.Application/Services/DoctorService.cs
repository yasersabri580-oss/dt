using AutoMapper;
using Doctor.Application.DTOs.Doctor;
using Doctor.Application.Interfaces;
using Doctor.Domain.Interfaces;

namespace Doctor.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorDto>> GetAllAsync()
    {
        var doctors = await _uow.Doctors.GetAllAsync();
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }

    public async Task<DoctorDto?> GetByIdAsync(Guid id)
    {
        var doctor = await _uow.Doctors.GetByIdAsync(id);
        return doctor == null ? null : _mapper.Map<DoctorDto>(doctor);
    }

    public async Task<DoctorWithProfileDto?> GetByIdWithProfileAsync(Guid id)
    {
        var doctor = await _uow.Doctors.GetByIdWithProfileAsync(id);
        return doctor == null ? null : _mapper.Map<DoctorWithProfileDto>(doctor);
    }

    public async Task<DoctorWithProfileDto?> GetByUserIdWithProfileAsync(long userId)
    {
        var doctor = await _uow.Doctors.GetByUserIdWithProfileAsync(userId);
        return doctor == null ? null : _mapper.Map<DoctorWithProfileDto>(doctor);
    }

    public async Task<DoctorDto> CreateAsync(CreateDoctorDto dto)
    {
        var user = await _uow.Users.GetByIdAsync(dto.UserId)
            ?? throw new InvalidOperationException("The specified user does not exist.");

        if (await _uow.Doctors.ExistsForUserAsync(dto.UserId))
            throw new InvalidOperationException("A doctor record already exists for this user.");

        var doctor = new Domain.Entities.Doctor
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            User = user,
            Slug = dto.Slug,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _uow.Doctors.AddAsync(doctor);
        await _uow.SaveChangesAsync();

        return _mapper.Map<DoctorDto>(doctor);
    }

    public async Task<DoctorDto?> UpdateAsync(Guid id, UpdateDoctorDto dto)
    {
        var doctor = await _uow.Doctors.GetByIdAsync(id);
        if (doctor == null) return null;

        if (dto.Slug != null) doctor.Slug = dto.Slug;
        if (dto.IsActive.HasValue) doctor.IsActive = dto.IsActive.Value;

        _uow.Doctors.Update(doctor);
        await _uow.SaveChangesAsync();

        return _mapper.Map<DoctorDto>(doctor);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var doctor = await _uow.Doctors.GetByIdAsync(id);
        if (doctor == null) return false;

        _uow.Doctors.Delete(doctor);
        await _uow.SaveChangesAsync();
        return true;
    }
}
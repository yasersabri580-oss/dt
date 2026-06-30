using AutoMapper;
using Doctor.Application.DTOs.DoctorProfile;
using Doctor.Application.Interfaces;
using Doctor.Domain.Interfaces;

namespace Doctor.Application.Services;

public class DoctorProfileService : IDoctorProfileService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public DoctorProfileService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<DoctorProfileDto?> GetByDoctorIdAsync(Guid doctorId)
    {
        var profile = await _uow.DoctorProfiles.GetByDoctorIdAsync(doctorId);
        return profile == null ? null : _mapper.Map<DoctorProfileDto>(profile);
    }

    public async Task<DoctorProfileDto> CreateAsync(CreateDoctorProfileDto dto)
    {
        var doctor = await _uow.Doctors.GetByIdAsync(dto.DoctorId)
            ?? throw new InvalidOperationException("The specified doctor does not exist.");

        if (await _uow.DoctorProfiles.ExistsForDoctorAsync(dto.DoctorId))
            throw new InvalidOperationException("A profile already exists for this doctor.");

        var profile = _mapper.Map<Domain.Entities.DoctorProfile>(dto);
        profile.UpdatedAt = DateTime.UtcNow;
        profile.CreatedAt = DateTime.UtcNow;

        await _uow.DoctorProfiles.AddAsync(profile);
        await _uow.SaveChangesAsync();

        return _mapper.Map<DoctorProfileDto>(profile);
    }

    public async Task<DoctorProfileDto?> UpdateAsync(Guid doctorId, UpdateDoctorProfileDto dto)
    {
        var profile = await _uow.DoctorProfiles.GetByDoctorIdAsync(doctorId);
        if (profile == null) return null;

        _mapper.Map(dto, profile);
        profile.UpdatedAt = DateTime.UtcNow;

        _uow.DoctorProfiles.Update(profile);
        await _uow.SaveChangesAsync();

        return _mapper.Map<DoctorProfileDto>(profile);
    }

    public async Task<bool> DeleteAsync(Guid doctorId)
    {
        var profile = await _uow.DoctorProfiles.GetByDoctorIdAsync(doctorId);
        if (profile == null) return false;

        _uow.DoctorProfiles.Delete(profile);
        await _uow.SaveChangesAsync();
        return true;
    }
}
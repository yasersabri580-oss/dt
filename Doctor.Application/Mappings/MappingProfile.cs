using AutoMapper;
using Doctor.Application.DTOs.Doctor;
using Doctor.Application.DTOs.DoctorProfile;
using Doctor.Application.DTOs.User;
using Doctor.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ── Users ────────────────────────────────────────────────────────
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Doctors ──────────────────────────────────────────────────────
        CreateMap<Domain.Entities.Doctor, DoctorDto>();
        CreateMap<Domain.Entities.Doctor, DoctorWithProfileDto>()
            .ForMember(d => d.Profile, opt => opt.MapFrom(s => s.Profile));

        // ── Doctor Profiles ─────────────────────────────────────────────
        CreateMap<DoctorProfile, DoctorProfileDto>();
        CreateMap<CreateDoctorProfileDto, DoctorProfile>();
        CreateMap<UpdateDoctorProfileDto, DoctorProfile>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
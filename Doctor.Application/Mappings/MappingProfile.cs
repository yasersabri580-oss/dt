
using AutoMapper;
using Doctor.Application.DTOs.User;
using Doctor.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
    
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}

using Doctor.Application.Interfaces;
using Doctor.Application.Mappings;
using Doctor.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IDoctorProfileService, DoctorProfileService>();

        return services;
    }
}
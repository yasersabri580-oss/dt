using Accounting.Application.Interfaces;
using Accounting.Application.Mappings;
using Accounting.Application.Services;
using Accounting.Application.Validators;

namespace Accounting.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
      
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
   
        services.AddScoped<IUserService, UserService>();
       
        return services;
    }
}

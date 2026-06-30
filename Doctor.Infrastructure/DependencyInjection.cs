
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Doctor.Infrastructure.ExternalAuth;
using Doctor.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Doctor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors()
                   .LogTo(
                       Console.WriteLine,
                       LogLevel.Information));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // External OAuth providers (registered as IExternalAuthProvider; resolved via IEnumerable<>).
        services.AddHttpClient<IExternalAuthProvider, GoogleAuthProvider>();
       

        return services;
    }
}

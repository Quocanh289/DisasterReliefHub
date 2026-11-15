using DisasterRelief.Application.Interfaces;
using DisasterRelief.Infrastructure.Persistence;
using DisasterRelief.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DisasterRelief.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured.");

        services.AddDbContext<DisasterReliefDbContext>(options =>
            options.UseNpgsql(connectionString, provider => provider.UseNetTopologySuite()));
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IReliefRequestRepository, ReliefRequestRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
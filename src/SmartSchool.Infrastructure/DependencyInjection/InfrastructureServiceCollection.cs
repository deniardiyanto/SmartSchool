using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Infrastructure.Persistence.Context;

using SmartSchool.Application.Common.Interfaces;
using SmartSchool.Application.Common.Settings;

using SmartSchool.Infrastructure.Security;
using SmartSchool.Infrastructure.Services;
using SmartSchool.Application.Features.Authentication.Login;

namespace SmartSchool.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
{
    services.Configure<JwtSettings>(
        configuration.GetSection(JwtSettings.SectionName));

    services.AddDbContext<SmartSchoolDbContext>(options =>
        options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection")));

    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddScoped<ICurrentUserService, CurrentUserService>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddScoped<IAuthService, AuthService>();

    return services;
}
    
}
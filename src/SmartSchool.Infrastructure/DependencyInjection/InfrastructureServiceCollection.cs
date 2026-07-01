using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Infrastructure.Persistence.Context;
using SmartSchool.Application.Features.ClassRooms.Interfaces;
using SmartSchool.Infrastructure.Services.Master;

using SmartSchool.Application.Common.Interfaces;
using SmartSchool.Application.Common.Settings;

using SmartSchool.Infrastructure.Security;
using SmartSchool.Infrastructure.Services.Authentication;
using SmartSchool.Application.Features.Authentication.Login;
using SmartSchool.Infrastructure.Services;

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
services.AddHttpContextAccessor();
    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddScoped<ICurrentUserService, CurrentUserService>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IClassRoomService, ClassRoomService>();

    return services;
}
    
}
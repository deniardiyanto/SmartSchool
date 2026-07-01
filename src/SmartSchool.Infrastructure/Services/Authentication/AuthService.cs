// using Microsoft.EntityFrameworkCore;
// using SmartSchool.Application.Common.Interfaces;
// using SmartSchool.Application.Common.Models;
// using SmartSchool.Application.Features.Authentication.Login;
// using SmartSchool.Infrastructure.Persistence.Context;

// namespace SmartSchool.Infrastructure.Services;

// public class AuthService : IAuthService
// {
//     private readonly SmartSchoolDbContext _context;
//     private readonly IPasswordHasher _passwordHasher;
//     private readonly IJwtTokenGenerator _jwtTokenGenerator;

//     public AuthService(
//         SmartSchoolDbContext context,
//         IPasswordHasher passwordHasher,
//         IJwtTokenGenerator jwtTokenGenerator)
//     {
//         _context = context;
//         _passwordHasher = passwordHasher;
//         _jwtTokenGenerator = jwtTokenGenerator;
//     }

//     public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
//     {
//         var user = await _context.Users
//             .Include(x => x.Role)
//             .FirstOrDefaultAsync(x =>
//                 x.Username == request.Username &&
//                 x.IsActive);

//         if (user == null)
//             return ApiResponse<LoginResponse>.Fail("Username atau password salah.");

//         if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
//             return ApiResponse<LoginResponse>.Fail("Username atau password salah.");

//         user.LastLogin = DateTime.UtcNow;

//         await _context.SaveChangesAsync();

//         var token = _jwtTokenGenerator.GenerateToken(user);

//         return ApiResponse<LoginResponse>.Ok(new LoginResponse
//         {
//             Token = token,
//             Username = user.Username,
//             FullName = user.FullName,
//             Role = user.Role.Name,
//             ExpiredAt = DateTime.UtcNow.AddHours(8)
//         });
//     }
// }
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartSchool.Application.Common.Exceptions;
using SmartSchool.Application.Common.Interfaces;
using SmartSchool.Application.Common.Settings;
using SmartSchool.Application.Features.Authentication.Login;
using SmartSchool.Infrastructure.Persistence.Context;

namespace SmartSchool.Infrastructure.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly SmartSchoolDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        SmartSchoolDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IOptions<JwtSettings> jwtOptions)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x =>
                x.Username == request.Username &&
                !x.IsDeleted);

        if (user == null)
            throw new UnauthorizedException("Username atau password salah.");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Username atau password salah.");

        if (!user.IsActive)
            throw new UnauthorizedException("User tidak aktif.");

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role.Name,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes)
        };
    }
}
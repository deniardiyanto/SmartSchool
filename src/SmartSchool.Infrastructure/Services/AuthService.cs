using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Common.Interfaces;
using SmartSchool.Application.Common.Models;
using SmartSchool.Application.Features.Authentication.Login;
using SmartSchool.Infrastructure.Context;

namespace SmartSchool.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly SmartSchoolDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        SmartSchoolDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x =>
                x.Username == request.Username &&
                x.IsActive);

        if (user == null)
            return ApiResponse<LoginResponse>.Fail("Username atau password salah.");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return ApiResponse<LoginResponse>.Fail("Username atau password salah.");

        user.LastLogin = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return ApiResponse<LoginResponse>.Ok(new LoginResponse
        {
            Token = token,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role.Name,
            ExpiredAt = DateTime.UtcNow.AddHours(8)
        });
    }
}
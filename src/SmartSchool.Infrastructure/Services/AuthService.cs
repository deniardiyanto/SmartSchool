// using Microsoft.EntityFrameworkCore;
// using SmartSchool.Application.DTOs;
// using SmartSchool.Application.Services;
// using SmartSchool.Infrastructure.Context;

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

//     public async Task<LoginResponse?> LoginAsync(LoginRequest request)
//     {
//         var user = await _context.Users
//             .Include(x => x.Role)
//             .FirstOrDefaultAsync(x => x.Username == request.Username);

//         if (user == null)
//             return null;

//         if (!user.IsActive)
//             return null;

//         var validPassword = _passwordHasher.Verify(
//             request.Password,
//             user.PasswordHash);

//         if (!validPassword)
//             return null;

//         var token = _jwtTokenGenerator.GenerateToken(user);

//         return new LoginResponse
//         {
//             Token = token,
//             FullName = user.FullName,
//             Role = user.Role.Name
//         };
//     }
// }
using SmartSchool.Application.Common.Models;

namespace SmartSchool.Application.Features.Authentication.Login;

public interface IAuthService
{
    Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request);
}
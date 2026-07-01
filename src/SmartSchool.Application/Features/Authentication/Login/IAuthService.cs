namespace SmartSchool.Application.Features.Authentication.Login;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
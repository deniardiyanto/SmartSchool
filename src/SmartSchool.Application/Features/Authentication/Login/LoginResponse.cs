namespace SmartSchool.Application.Features.Authentication.Login;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
     public string Username { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public DateTime ExpiredAt { get; set; }
}
namespace SmartSchool.Application.Features.Guardians.Contracts;

public class CreateGuardianRequest
{
    public string GuardianCode { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
}
namespace SmartSchool.Application.Features.Guardians.Contracts;

public class GuardianFilterRequest
{
    public string? Keyword { get; set; }

    public bool? IsActive { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
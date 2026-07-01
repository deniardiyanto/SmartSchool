namespace SmartSchool.Application.Features.Guardians.Contracts;

public class PagedGuardianResponse
{
    public IReadOnlyList<GuardianDto> Items { get; set; }
        = new List<GuardianDto>();

    public int TotalRecords { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
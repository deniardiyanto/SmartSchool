using SmartSchool.Application.Features.Guardians.Contracts;

namespace SmartSchool.Application.Features.Guardians.Interfaces;

public interface IGuardianService
{
    Task<PagedGuardianResponse> GetPagedAsync(
        GuardianFilterRequest request);

    Task<GuardianDto> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(
        CreateGuardianRequest request);

    Task UpdateAsync(
        Guid id,
        UpdateGuardianRequest request);

    Task DeleteAsync(Guid id);
}
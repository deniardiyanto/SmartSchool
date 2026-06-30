using SmartSchool.Application.Common.Interfaces;

namespace SmartSchool.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId => Guid.Empty;

    public string Username => string.Empty;

    public string FullName => string.Empty;

    public string Role => string.Empty;

    public bool IsAuthenticated => false;
}
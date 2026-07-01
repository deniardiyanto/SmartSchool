// using SmartSchool.Application.Common.Interfaces;

// namespace SmartSchool.Infrastructure.Services;

// public class CurrentUserService : ICurrentUserService
// {
//     public Guid UserId => Guid.Empty;

//     public string Username => string.Empty;

//     public string FullName => string.Empty;

//     public string Role => string.Empty;

//     public bool IsAuthenticated => false;
// }
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SmartSchool.Application.Common.Interfaces;

namespace SmartSchool.Infrastructure.Security;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(id, out var guid))
                return guid;

            return null;
        }
    }

    public string? Username =>
        _httpContextAccessor.HttpContext?
            .User?
            .Identity?
            .Name;

   public bool IsAuthenticated =>
    _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
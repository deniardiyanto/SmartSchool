// namespace SmartSchool.Application.Common.Interfaces;

// public interface ICurrentUserService
// {
//     Guid UserId { get; }

//     string Username { get; }

//     string FullName { get; }

//     string Role { get; }

//     bool IsAuthenticated { get; }
// }
namespace SmartSchool.Application.Common.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }

    string? Username { get; }

    bool IsAuthenticated { get; }
}
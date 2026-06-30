using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
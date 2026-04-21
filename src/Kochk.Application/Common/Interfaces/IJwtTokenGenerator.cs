using Kochk.Domain.Entities.Identity;

namespace Kochk.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(AppUser user, IEnumerable<string> roles);
}


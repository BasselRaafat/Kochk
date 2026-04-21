using Kochk.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Kochk.Application.Common.Interfaces.Services;

public interface IAuthServices
{
    public Task<string> GenerateTokenAsync(AppUser user, UserManager<AppUser> userManager);
}

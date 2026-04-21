using System.Security.Claims;
using Kochk.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Kochk.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid UserId =>
        Guid.Parse(
            httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? Guid.Empty.ToString()
        );

    public string? Email => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    // public bool IsAuthenticated =>
    //     httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}

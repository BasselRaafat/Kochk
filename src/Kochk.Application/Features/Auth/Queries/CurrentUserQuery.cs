using Kochk.Application.Common.Interfaces;
using Kochk.Application.Common.Models;
using Kochk.Application.Features.Auth.Models;
using Kochk.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kochk.Application.Features.Auth.Queries;

public record CurrentUserQuery(Guid UserId) : IRequest<Result<SignInResponse>>;

public class CurrentUserQueryHandler(
    UserManager<AppUser> userManager,
    IJwtTokenGenerator tokenGenerator
) : IRequestHandler<CurrentUserQuery, Result<SignInResponse>>
{
    public async Task<Result<SignInResponse>> Handle(
        CurrentUserQuery request,
        CancellationToken cancellationToken
    )
    {
        AppUser? user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Errors.User.NotFound(request.UserId);
        var roles = await userManager.GetRolesAsync(user!);
        var token = await tokenGenerator.GenerateTokenAsync(user, roles);
        return new SignInResponse() { Email = user.Email!, Token = token };
    }
}

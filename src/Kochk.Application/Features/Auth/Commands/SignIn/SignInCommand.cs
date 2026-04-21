using Kochk.Application.Common.Interfaces;
using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Models;
using Kochk.Application.Features.Auth.Models;
using Kochk.Domain.Entities;
using Kochk.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Kochk.Application.Features.Auth.Commands.SignIn;

public record SignInCommand(string Email, string Password) : IRequest<Result<SignInResponse>>;

public class SignInCommandHandler(
    UserManager<AppUser> UserManager,
    SignInManager<AppUser> SignInManager,
    IReadRepository<BusinessUser> _userRepo,
    IJwtTokenGenerator TokenGenerator,
    ILogger<SignInCommandHandler> logger
) : IRequestHandler<SignInCommand, Result<SignInResponse>>
{
    async Task<Result<SignInResponse>> IRequestHandler<
        SignInCommand,
        Result<SignInResponse>
    >.Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling sign-in request for email: {Email}", request.Email);

        var user = await UserManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            logger.LogWarning("Sign-in failed: no user found with email {Email}", request.Email);
            return Errors.Auth.InvalidCredentials;
        }

        var businessUser = await _userRepo.GetByIdAsync(user.Id,cancellationToken);
        if (businessUser is null)
        {
            logger.LogError(
                "Sign-in failed: user with ID {UserId} found in identity but not in business repository",
                user.Id
            );
            return Errors.User.NotFound(user.Id);
        }

        var result = await SignInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            logger.LogWarning("Sign-in failed: invalid password for user {Email}", request.Email);
            return Errors.Auth.InvalidCredentials;
        }

        var roles = await UserManager.GetRolesAsync(user);
        var token = await TokenGenerator.GenerateTokenAsync(user, roles);

        logger.LogInformation(
            "User {Email} signed in successfully with roles: {Roles}",
            request.Email,
            string.Join(", ", roles)
        );

        return new SignInResponse
        {
            Email = user.Email!,
            Token = token,
            DisplayName = businessUser.DisplayName,
            Role = string.Join(",", roles),
        };
    }
}

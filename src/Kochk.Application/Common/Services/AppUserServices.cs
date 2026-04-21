using Kochk.Application.Common.Models;
using Kochk.Application.Features.Auth.Models;
using Kochk.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Kochk.Application.Common.Services;

public class AppUserServices
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AppUserServices> _logger;

    public AppUserServices(UserManager<AppUser> userManager, ILogger<AppUserServices> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Result<AppUser>> CreateAppUserAsync(UserInfo user)
    {
        AppUser? exsistingEmail = await _userManager.FindByEmailAsync(user.Email);
        if (exsistingEmail is not null)
        {
            _logger.LogWarning("Registration failed: Email {Email} already exists", user.Email);
            return Errors.Auth.DuplicateEmail;
        }

        var appUser = new AppUser()
        {
            Email = user.Email,
            UserName = user.Email,
            PhoneNumber = user.PhoneNumber,
        };
        var userResult = await _userManager.CreateAsync(appUser, user.Password);
        if (!userResult.Succeeded)
        {
            _logger.LogWarning(
                "Creating User As A Customer Failed With Errors: {erros}",
                string.Join(", ", userResult.Errors.Select(e => $"{e.Code}: {e.Description}"))
            );

            return Errors.Auth.Validation(
                userResult.Errors.ToDictionary(
                    entry => entry.Code,
                    entry => (List<string>)[entry.Description]
                )
            );
        }
        return appUser;
    }
}

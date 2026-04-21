using AutoMapper;
using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Application.Common.Models;
using Kochk.Application.Common.Services;
using Kochk.Domain.Common;
using Kochk.Domain.Entities;
using Kochk.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Kochk.Application.Features.Auth.Commands.CustomerSignUp;

public class CustomerSignUpCommandHandler(
    AppUserServices appUserServices,
    UserManager<AppUser> userManager,
    IWriteRepository<BusinessUser> bussnissUserRepo,
    IUnitOfWork unitOfWork,
    ILogger<CustomerSignUpCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CustomerSignUpCommand, Result>
{
    public async Task<Result> Handle(
        CustomerSignUpCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await appUserServices.CreateAppUserAsync(request.UserInfo);
        if (!result.IsSuccess)
            return result;
        var roleResult = await userManager.AddToRoleAsync(result.Value, Roles.Cusomter);
        if (!roleResult.Succeeded)
        {
            logger.LogError(
                "Failed to add user {Email} to Customer role. Errors: {Errors}. Rolling back user creation.",
                request.UserInfo.Email,
                string.Join(", ", roleResult.Errors.Select(e => $"{e.Code}: {e.Description}"))
            );
            await userManager.DeleteAsync(result.Value);
            return Errors.Auth.DuplicateEmail;
        }

        BusinessUser bussnissUser = new()
        {
            Id = result.Value.Id,
            Email = request.UserInfo.Email,
            PhoneNumber = request.UserInfo.PhoneNumber,
            DisplayName = request.UserInfo.FirstName + " " + request.UserInfo.LastName,
            Role = Role.Customer,
            Addresses = [mapper.Map<UserAddress>(request.Address)],
        };
        try
        {
            await bussnissUserRepo.CreateAsync(bussnissUser, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            logger.LogInformation(
                "Successfully created customer account for {Email}",
                request.UserInfo.Email
            );

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Failed to create business user with email {email}. Rolling back Identity user.",
                request.UserInfo.Email
            );
            await userManager.DeleteAsync(result.Value); // rollback user creation
            return Errors.Auth.BusinessUserCreationFailed;
        }
    }
}

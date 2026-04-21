using Kochk.Application.Common.Interfaces;
using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Application.Common.Models;
using Kochk.Application.Common.Models;
using Kochk.Domain.Entities;
using MediatR;

namespace Kochk.Application.Features.User.Commands;

public record UpdateUserAddressCommand(
    Guid UserId,
    Guid AddressId,
    string Appartment,
    string Street,
    string City,
    string Country
) : IRequest<Result>;

public class UpdateUserAddressCommandHandler(
    IWriteRepository<UserAddress> addressRepo,
    IUnitOfWork unitOfWork
// ,ICurrentUserService currentUser
) : IRequestHandler<UpdateUserAddressCommand, Result>
{
    public async Task<Result> Handle(
        UpdateUserAddressCommand request,
        CancellationToken cancellationToken
    )
    {
        var address = await addressRepo.GetByIdAsync(request.AddressId, cancellationToken);

        if (address is null)
            return Errors.User.NotFoundAddress(request.AddressId, request.UserId);

        address.Appartment = request.Appartment;
        address.City = request.City;
        address.Country = request.Country;
        address.Street = request.Street;
        addressRepo.Update(address);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

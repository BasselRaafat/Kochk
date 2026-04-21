using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Application.Common.Models;
using Kochk.Application.Common.Specifications.CartSpecifications;
using Kochk.Domain.Entities;
using MediatR;

namespace Kochk.Application.Features.Carts.Commands;

public record UpdateCartItemQuantityCommand(Guid UserId, Guid ItemId, int NewQuantity)
    : IRequest<Result>;

public class UpdateCartItemQuantityCommandHandler(
    IWriteRepository<Cart> cartRepo,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateCartItemQuantityCommand, Result>
{
    public async Task<Result> Handle(
        UpdateCartItemQuantityCommand request,
        CancellationToken cancellationToken
    )
    {
        var cartSpec = new CartSpecification(request.UserId);
        var cart = await cartRepo.GetWithSpecAsync(cartSpec, cancellationToken);
        if (cart is null)
            return Errors.Carts.NotFound(request.UserId);
        cart.UpdateQuantity(request.ItemId, request.NewQuantity);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}


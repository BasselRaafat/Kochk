using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Application.Common.Models;
using Kochk.Application.Common.Specifications.CartSpecifications;
using Kochk.Domain.Entities;
using MediatR;

namespace Kochk.Application.Features.Carts.Commands;

public record DeleteCartItemCommand(Guid UserId, Guid CartItemId) : IRequest<Result>;

public class DeleteCartCommandHandler(IWriteRepository<Cart> cartRepo, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCartItemCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCartItemCommand request,
        CancellationToken cancellationToken
    )
    {
        var cartSpec = new CartSpecification(request.UserId);
        var cart = await cartRepo.GetWithSpecAsync(cartSpec, cancellationToken);
        if (cart is null)
            return Errors.Carts.NotFound(request.UserId);
        cart.RemoveItem(request.CartItemId);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

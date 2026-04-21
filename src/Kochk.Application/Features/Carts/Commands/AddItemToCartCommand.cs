using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Application.Common.Models;
using Kochk.Application.Common.Specifications.CartSpecifications;
using Kochk.Domain.Entities;
using MediatR;

namespace Kochk.Application.Features.Carts.Commands;

public record CreateCartCommand(Guid UserId, Guid ProductId) : IRequest<Result>;

public class CreateCartCommandHandler(
    IWriteRepository<Cart> cartRepo,
    IReadRepository<Product> productRepo,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateCartCommand, Result>
{
    public async Task<Result> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepo.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return Errors.Product.NotFound(request.ProductId);

        var cartItem = new CartItem(product);

        var cartSpec = new CartSpecification(request.UserId);
        var cart = await cartRepo.GetWithSpecAsync(cartSpec, cancellationToken);

        if (cart is null)
        {
            cart = new Cart(request.UserId);
            await cartRepo.CreateAsync(cart, cancellationToken);
        }

        cart.AddItem(cartItem);
        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

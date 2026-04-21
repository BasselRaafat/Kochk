using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Models;
using Kochk.Application.Common.Specifications.CartSpecifications;
using Kochk.Domain.Entities;
using MediatR;

namespace Kochk.Application.Features.Carts.Queries;

public record GetCartCommand(Guid UserId) : IRequest<Result<Cart>>;

public class GetCartCommandHandler(IReadRepository<Cart> cartRepo)
    : IRequestHandler<GetCartCommand, Result<Cart>>
{
    public async Task<Result<Cart>> Handle(
        GetCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var cartSpec = new CartSpecification(request.UserId);
        var cart = await cartRepo.GetWithSpecAsync(cartSpec, cancellationToken);
        if (cart is null)
            return Errors.Carts.NotFound(request.UserId);

        return cart;
    }
}

using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Models;
using Kochk.Application.Common.Specifications.CartSpecifications;
using Kochk.Domain.Entities;
using Kochk.Domain.Entities.OrderAggregate;
using MediatR;

namespace Kochk.Application.Features.Orders.Commands;

public record CreateOrderCommand(Guid UserId, Guid DeliveryMethodId) : IRequest<Result>;

public class CreateOrderCommandHandler(
    IReadRepository<Cart> _CartRepo
// ,IWriteRepository<Order> orderRepo
) : IRequestHandler<CreateOrderCommand, Result>
{
    public async Task<Result> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken
    )
    {
        CartSpecification cartSpec = new(request.UserId);
        Cart? cart = await _CartRepo.GetWithSpecAsync(cartSpec, cancellationToken);
        if (cart is null)
            return Errors.Carts.NotFound(request.UserId);
        return Errors.Carts.NotFound(request.UserId);
    }
}

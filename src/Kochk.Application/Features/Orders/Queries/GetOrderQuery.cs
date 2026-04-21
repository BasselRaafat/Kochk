using Kochk.API.Dtos;
using Kochk.Application.Common.Models;
using MediatR;

namespace Kochk.Application.Features.Orders.Queries;

public record GetOrderQuery() : IRequest<Result<OrderToReturnDto>>;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result<OrderToReturnDto>>
{
    public Task<Result<OrderToReturnDto>> Handle(
        GetOrderQuery request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}


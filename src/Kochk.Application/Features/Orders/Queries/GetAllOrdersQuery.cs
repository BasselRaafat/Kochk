using Kochk.API.Dtos;
using Kochk.Application.Common.Models;
using MediatR;

namespace Kochk.Application.Features.Orders.Queries;

public record GetAllOrdersQuery() : IRequest<Result<IReadOnlyList<OrderToReturnDto>>>;

public class GetAllOrdersQueryHandler
    : IRequestHandler<GetAllOrdersQuery, Result<IReadOnlyList<OrderToReturnDto>>>
{
    public Task<Result<IReadOnlyList<OrderToReturnDto>>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}


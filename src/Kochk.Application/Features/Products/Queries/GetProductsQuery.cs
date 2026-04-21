using Kochk.Application.Common.Models;
using Kochk.Application.Features.Products.DTOs;
using MediatR;

namespace Kochk.Application.Features.Products.Queries;

public record GetProductsQuery() : IRequest<Result<IReadOnlyList<ProductDto>>>;

public class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, Result<IReadOnlyList<ProductDto>>>
{
    public Task<Result<IReadOnlyList<ProductDto>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}


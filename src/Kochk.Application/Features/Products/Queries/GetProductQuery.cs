using Kochk.Application.Common.Models;
using Kochk.Application.Features.Products.DTOs;
using MediatR;

namespace Kochk.Application.Features.Products.Queries;

public record GetProductQuery() : IRequest<Result<ProductDto>>;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductDto>>
{
    public Task<Result<ProductDto>> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}


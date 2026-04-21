using AutoMapper;
using Kochk.API.Dtos;
using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.API.Helpers;

public class ProductItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
{
    private readonly IConfiguration _configuration;

    public ProductItemPictureUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(
        OrderItem source,
        OrderItemDto destination,
        string destMember,
        ResolutionContext context
    )
    {
        if (string.IsNullOrEmpty(source.Product.DefaultPictureUrl))
            return string.Empty;
        return $"{_configuration["BaseApiUrl"]}/{source.Product.DefaultPictureUrl}";
    }
}

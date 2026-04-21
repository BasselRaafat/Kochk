using AutoMapper;
using Kochk.Application.Features.Products.DTOs;
using Kochk.Domain.Entities;

namespace Kochk.API.Helpers;

public class ProductPictureResolver : IValueResolver<Product, ProductDto, string>
{
    private readonly IConfiguration _configuration;

    public ProductPictureResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(
        Product source,
        ProductDto destination,
        string destMember,
        ResolutionContext context
    )
    {
        if (string.IsNullOrEmpty(source.DefaultPictureUrl))
            return string.Empty;
        return $"{_configuration["BaseApiUrl"]}/{source.DefaultPictureUrl}";
    }
}

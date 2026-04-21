using AutoMapper;
using Kochk.API.Errors;
using Kochk.API.Helpers;
using Kochk.Application.Common.Interfaces.Services;
using Kochk.Application.Common.Specifications.ProductSpecs;
using Kochk.Application.Features.Products.DTOs;
using Kochk.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kochk.API.Controllers;

[Authorize]
public class ProductController : BaseApiController
{
    private readonly IProductService _productService;
    private readonly IMapper _mppper;

    public ProductController(IProductService productService, IMapper mppper)
    {
        _mppper = mppper;
        _productService = productService;
    }

    [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductDto>>> Get(
        [FromQuery] ProductSpecsParam specParams
    )
    {
        var products = await _productService.GetProductsAsync(specParams);
        var productsDto = _mppper.Map<IEnumerable<Product>, IReadOnlyList<ProductDto>>(products);
        var count = await _productService.GetCountAsync(specParams);
        return Ok(
            new Pagination<ProductDto>(
                specParams.PageSize,
                specParams.PageIndex,
                count,
                productsDto
            )
        );
    }

    [ProducesResponseType(typeof(ProductDto), 200)]
    [ProducesResponseType(typeof(ApiError), 404)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get(Guid id)
    {
        var product = await _productService.GetProductAsync(id);
        if (product is null)
            return NotFound(new ApiError(404));
        return Ok(_mppper.Map<Product, ProductDto>(product));
    }
}

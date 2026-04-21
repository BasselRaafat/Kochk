namespace Kochk.Application.Features.Products.DTOs;

public class ProductDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public string Category { get; set; } = default!;
    public string Brand { get; set; } = default!;
}

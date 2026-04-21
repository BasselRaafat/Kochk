using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string DefaultPictureUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public Guid VendorId { get; set; }

    public Vendor Vendor { get; set; } = default!;
    public ProductCategory Category { get; set; } = default!;
    public ProductBrand Brand { get; set; } = default!;
    public ICollection<CartItem> CartItems { get; set; } = default!;
    public ICollection<OrderItem> OrderItems { get; set; } = default!;
    public ICollection<ProductPictureUrl> PictureUrls { get; set; } = default!;
    public ICollection<ProductReview> Reviews { get; set; } = default!;
}

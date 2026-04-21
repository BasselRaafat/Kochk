namespace Kochk.Domain.Entities;

public class ProductPictureUrl : BaseEntity
{
    public Guid PorductId { get; set; }
    public Product Product { get; set; } = default!;
    public string ProductUrl { get; set; } = default!;
}

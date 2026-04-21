namespace Kochk.Domain.Entities;

public class ProductReview : BaseEntity
{
    public string Comment { get; set; } = default!;
    public int Rate { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    public BusinessUser User { get; set; } = default!;
    public Product Product { get; set; } = default!;
}

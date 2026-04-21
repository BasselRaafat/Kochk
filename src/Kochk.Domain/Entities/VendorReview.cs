namespace Kochk.Domain.Entities;

public class VendorReview : BaseEntity
{
    public string Comment { get; set; } = default!;
    public int Rate { get; set; }

    public Guid CustomerId { get; set; }
    public Guid VendorId { get; set; }

    public BusinessUser Customer { get; set; } = default!;
    public Vendor Vendor { get; set; } = default!;
}

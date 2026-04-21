using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.Domain.Entities;

public class Vendor : BaseEntity
{
    public string BussnessisName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal CommissionRate { get; set; } = 0.9M;
    public decimal Rate { get; set; }
    public VendorStatus Status { get; set; } = VendorStatus.PendingApproval;

    public BusinessUser User { get; set; } = default!;
    public ICollection<VendorReview> Reviews { get; set; } = [];
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<VendorOrder> Orders { get; set; } = [];
}

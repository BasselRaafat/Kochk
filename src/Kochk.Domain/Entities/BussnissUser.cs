using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.Domain.Entities;

public class BusinessUser : BaseEntity
{
    public string DisplayName { set; get; } = default!;
    public string Email { set; get; } = default!;
    public string PhoneNumber { set; get; } = default!;
    public Role Role { set; get; }
    public Vendor Vendor { get; set; } = default!;
    public Cart Cart { get; set; } = default!;
    public ICollection<UserAddress> Addresses { set; get; } = [];
    public ICollection<ProductReview> Reviews { set; get; } = [];
    public ICollection<Order> Orders { set; get; } = [];
}

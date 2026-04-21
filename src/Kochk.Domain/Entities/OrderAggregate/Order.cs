namespace Kochk.Domain.Entities.OrderAggregate;

public class Order : BaseEntity
{
    public Order() { }

    public Order(Guid userId, List<(Product, int)> products, Guid addressId, Guid deliveryMethodId)
    {
        UserId = userId;
        var productsGrouped = products.GroupBy(p => p.Item1.VendorId);
        foreach (var product in productsGrouped)
        {
            VendorOrders.Add(new VendorOrder(product.ToList(), Id));
        }
        AddressId = addressId;
        DeliveryMethodId = deliveryMethodId;
        OrderStatus = OrderStatus.Processing;
        PaymentStatus = PaymentStatus.Pending;
        OrderDate = DateTimeOffset.UtcNow;
        SubTotal = products.Select(p => p.Item1.Price * p.Item2).Sum();
        PaymentIntent = "";
    }

    public decimal SubTotal { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public string PaymentIntent { get; set; } = default!;
    public Guid DeliveryMethodId { get; set; }
    public Guid UserId { get; set; } = default;
    public Guid AddressId { get; set; } = default;

    public DeliveryMethod DeliveryMethod { get; set; } = default!;
    public UserAddress Address { get; set; } = default!;
    public BusinessUser User { get; set; } = default!;
    public ICollection<VendorOrder> VendorOrders { get; set; } = [];

    public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
}

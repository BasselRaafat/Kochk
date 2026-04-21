namespace Kochk.Domain.Entities.OrderAggregate;

public class VendorOrder : BaseEntity
{
    public Guid VendorId { get; set; }
    public Guid OrderId { get; set; }
    public Vendor Vendor { get; set; } = default!;
    public Order Order { get; set; } = default!;
    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> Items { get; set; } = [];

    public VendorOrder() { }

    public VendorOrder(List<(Product, int)> products, Guid orderId)
    {
        OrderId = orderId;
        Status = OrderStatus.Processing;
        var productsGrouped = products.GroupBy(p => p.Item1.VendorId);
        if (products.Count != productsGrouped.Count())
            throw new ArgumentException("Prodcuts Not From the same Vendor");
        VendorId = products[0].Item1.VendorId;
        foreach (var product in products)
        {
            Items.Add(new OrderItem(product.Item1, product.Item2));
        }
    }
}

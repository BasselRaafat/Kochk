namespace Kochk.Domain.Entities.OrderAggregate;

public class OrderItem : BaseEntity
{
    public OrderItem() { }

    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
        Price = product.Price;
    }

    public Guid VendorOrderId { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Product Product { get; set; } = default!;
    public VendorOrder VendorOrder { get; set; } = default!;
}

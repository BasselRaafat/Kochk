namespace Kochk.Domain.Entities;

public class CartItem : BaseEntity
{
    public string ProductName { get; private set; } = default!;
    public int Quantity { get; private set; } = default;
    public decimal UnitPrice { get; private set; }
    public decimal TotalCost => UnitPrice * Quantity;

    public Guid CartId { get; private set; } = default;
    public Guid ProductId { get; private set; } = default;

    public Product Product { get; private set; } = default!;
    public Cart Cart { get; private set; } = default!;

    private CartItem() { }

    public CartItem(Product product)
    {
        Product = product;
        ProductName = product.Name;
        Quantity = 1;
        UnitPrice = product.Price;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));
        Quantity = quantity;
    }
}

namespace Kochk.Domain.Entities;

public class Cart : BaseEntity
{
    public ICollection<CartItem> CartItems { get; private set; } = [];
    public Guid UserId { get; private set; }

    public BusinessUser User { get; private set; } = default!;

    // public decimal TotalCost { get; set; }
    public decimal TotalCost => CartItems.Sum(item => item.Product.Price * item.Quantity);

    public Cart() { }

    public Cart(Guid userId)
    {
        UserId = userId;
    }

    public Cart(Guid userId, CartItem item)
    {
        UserId = userId;
        CartItems.Add(item);
    }

    public Cart(Guid userId, ICollection<CartItem> items)
    {
        UserId = userId;
        CartItems = items;
    }

    public void AddItem(CartItem item)
    {
        var existingItem = CartItems.FirstOrDefault(i => i.ProductId == item.ProductId);

        if (existingItem != null)
            existingItem.UpdateQuantity(item.Quantity);
        else
            CartItems.Add(item);
    }

    public void RemoveItem(Guid cartItemId)
    {
        var item =
            CartItems.FirstOrDefault(i => i.Id == cartItemId)
            ?? throw new InvalidOperationException("Item not found in cart");
        CartItems.Remove(item);
    }

    public void UpdateQuantity(Guid cartItemId, int newQuantity)
    {
        var item =
            CartItems.FirstOrDefault(i => i.Id == cartItemId)
            ?? throw new InvalidOperationException("Item not found in cart");
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(newQuantity));

        if (newQuantity > 100) // Business rule
            throw new ArgumentException("Maximum quantity is 100", nameof(newQuantity));

        item.UpdateQuantity(newQuantity);
    }

    public void Clear()
    {
        CartItems.Clear();
    }
}

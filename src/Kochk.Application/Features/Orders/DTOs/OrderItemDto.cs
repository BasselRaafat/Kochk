namespace Kochk.API.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

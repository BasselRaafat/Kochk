using Kochk.Domain.Entities;

namespace Kochk.API.Dtos;

public class OrderToReturnDto
{
    public int Id { get; set; }
    public string BuyerEmail { get; set; } = default!;
    public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
    public UserAddress Address { get; set; } = default!;
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string OrderStatus { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public decimal DeliveryMethodCost { get; set; }
    public DateTimeOffset OrderDate { get; set; }
}

namespace Kochk.Domain.Entities.OrderAggregate;

public class DeliveryMethod : BaseEntity
{
    public DeliveryMethod() { }

    public DeliveryMethod(string name, string description, string deliveryTime, decimal cost)
    {
        Name = name;
        Description = description;
        DeliveryTime = deliveryTime;
        Cost = cost;
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string DeliveryTime { get; set; } = default!;
    public decimal Cost { get; set; }
}

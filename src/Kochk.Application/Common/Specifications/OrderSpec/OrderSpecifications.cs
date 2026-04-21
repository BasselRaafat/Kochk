using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.Application.Common.Specifications.OrderSpec;

public class OrderSpecifications : BaseSpecifications<Order>
{
    public OrderSpecifications(OrderSpecificationsParamters paramters)
        : base(O =>
            (!paramters.UserId.HasValue || paramters.UserId == O.UserId)
            && (!paramters.OrderId.HasValue || paramters.OrderId == O.Id)
        )
    {
        Includes.Add(O => O.DeliveryMethod);
        // Includes.Add(O => O.OrderItems);
        if (paramters.OrderId.HasValue)
            return;
        AddOrderByDec(O => O.OrderDate);
    }

    public OrderSpecifications(Guid userId)
        : base(O => O.UserId == userId)
    {
        Includes.Add(O => O.DeliveryMethod);
        // Includes.Add(O => O.OrderItems);
        AddOrderByDec(O => O.OrderDate);
    }

    public OrderSpecifications(Guid userId, Guid orderId)
        : base(O => O.UserId == userId && O.Id == orderId)
    {
        Includes.Add(O => O.DeliveryMethod);
        // Includes.Add(O => O.OrderItems);
    }
}

public class OrderSpecificationsParamters
{
    public Guid? UserId { get; set; }
    public Guid? OrderId { get; set; }
}

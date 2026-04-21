using Kochk.Domain.Entities;
using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.Application.Common.Interfaces.Services;

public interface IOrderService
{
    Task<Order?> CreateOrderAsync(
        Guid userId,
        Guid cartId,
        Guid deliveryMethodId,
        UserAddress address
    );
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(Guid userId);
    Task<Order?> GetOrderByIdForUserAsync(Guid userId, Guid orderId);
    Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
}

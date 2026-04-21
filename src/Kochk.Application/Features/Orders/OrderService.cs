using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.Services;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Domain.Entities;
using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.Application.Features;

public class OrderService : IOrderService
{
    // private readonly IWriteRepository<Cart> _cartRepo;
    // private readonly IUnitOfWork _unitOfWork;
    //
    // public OrderService(IWriteRepository<Cart> cartRepo, IUnitOfWork unitOfWork)
    // {
    //     _cartRepo = cartRepo;
    //     _unitOfWork = unitOfWork;
    // }
    //
    // public async Task<Order?> CreateOrderAsync(
    //     string buyerEmail,
    //     Guid cartId,
    //     int deliveryMethodId
    // // OrderAddress address
    // )
    // {
    //     var cart = await _cartRepo.GetByIdAsync(cartId);
    //     var productRepo = _unitOfWork.GetRepo<Product>();
    //     var orderItemsAwait =
    //         cart?.CartProducts.Select(async P =>
    //         {
    //             var product = await productRepo.GetAsync(P.Id);
    //             var productItem = new ProductItem(product.Id, product.Name, product.PictureUrl);
    //             return new OrderItem(productItem, P.Quantity, product.Price);
    //         }) ?? [];
    //     List<OrderItem> orderItems;
    //     if (orderItemsAwait.Any())
    //         orderItems = [.. await Task.WhenAll(orderItemsAwait)];
    //     else
    //         orderItems = [];
    //     var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);
    //     var deliveryMethod = await _unitOfWork.GetRepo<DeliveryMethod>().GetAsync(deliveryMethodId);
    //     var order = new Order(buyerEmail, orderItems, address, subTotal, deliveryMethod);
    //
    //     await _unitOfWork.GetRepo<Order>().CreateAsync(order);
    //     var result = await _unitOfWork.CompleteChangesAsync();
    //     if (result <= 0)
    //         return null;
    //     return order;
    // }
    //
    // public Task<Order?> CreateOrderAsync(
    //     Guid userId,
    //     Guid cartId,
    //     Guid deliveryMethodId,
    //     UserAddress address
    // )
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync() =>
    //     await _unitOfWork.GetRepo<DeliveryMethod>().GetAllAsync();
    //
    // public async Task<Order?> GetOrderByIdForUserAsync(string byerEmail, int orderId)
    // {
    //     var spec = new OrderSpecifications(byerEmail, orderId);
    //     var orderRepo = _unitOfWork.GetRepo<Order>();
    //     return await orderRepo.GetWithSpecAsync(spec);
    // }
    //
    // public Task<Order?> GetOrderByIdForUserAsync(Guid userId, Guid orderId)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string emailBuyer)
    // {
    //     var spce = new OrderSpecifications(emailBuyer);
    //     var orderRepo = _unitOfWork.GetRepo<Order>();
    //     return await orderRepo.GetAllWithSpecAsync(spce);
    // }
    //
    // public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(Guid userId)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // Task<IReadOnlyList<DeliveryMethod>> IOrderService.GetAllDeliveryMethodsAsync()
    // {
    //     throw new NotImplementedException();
    // }
    public Task<Order?> CreateOrderAsync(
        Guid userId,
        Guid cartId,
        Guid deliveryMethodId,
        UserAddress address
    )
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetOrderByIdForUserAsync(Guid userId, Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}

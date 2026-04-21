using System.Security.Claims;
using AutoMapper;
using Kochk.API.Dtos;
using Kochk.API.Errors;
using Kochk.Application.Common.Interfaces.Services;
using Kochk.Application.Features.User.DTOs;
using Kochk.Domain.Entities;
using Kochk.Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kochk.API.Controllers;

// [Authorize]
// public class OrderController : BaseApiController
// {
//     private readonly IOrderService _orderService;
//     private readonly IMapper _mapper;
//
//     public OrderController(IOrderService OrderService, IMapper mapper)
//     {
//         _orderService = OrderService;
//         _mapper = mapper;
//     }
//
//     [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
//     [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
//     [HttpPost]
//     public async Task<ActionResult<OrderToReturnDto>> Create(OrderDto model)
//     {
//         var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//         var mappedAddress = _mapper.Map<OrderAddressDto, UserAddress>(model.Address);
//         var order = await _orderService.CreateOrderAsync(
//             new Guid(userId!),
//             model.CartId,
//             model.DeliveryMethodId,
//             mappedAddress
//         );
//         if (order is null)
//             return BadRequest(new ApiError(400));
//         var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);
//         return Ok(mappedOrder);
//     }
//
//     [HttpGet]
//     public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrders()
//     {
//         var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//         var orders = await _orderService.GetOrdersForUserAsync(new Guid(userId!));
//         var mappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(
//             orders
//         );
//         return Ok(mappedOrders);
//     }
//
//     [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
//     [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
//     [HttpGet("{id}")]
//     public async Task<ActionResult<OrderToReturnDto>> GetOrder(Guid id)
//     {
//         var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//         var order = await _orderService.GetOrderByIdForUserAsync(new Guid(userId!), id);
//         if (order is null)
//             return BadRequest(new ApiError(400));
//         var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);
//         return Ok(mappedOrder);
//     }
//
//     [HttpGet("deliverymethods")]
//     public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
//     {
//         var deliveryMethods = await _orderService.GetAllDeliveryMethodsAsync();
//         return Ok(deliveryMethods);
//     }
// }

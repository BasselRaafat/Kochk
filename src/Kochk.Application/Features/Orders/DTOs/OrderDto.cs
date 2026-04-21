using System.ComponentModel.DataAnnotations;
using Kochk.Application.Features.User.DTOs;

namespace Kochk.API.Dtos;

public class OrderDto
{
    [Required]
    public Guid DeliveryMethodId { get; set; }

    [Required]
    public Guid CartId { get; set; }
    public OrderAddressDto Address { get; set; } = default!;
}

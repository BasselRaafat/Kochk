using System.ComponentModel.DataAnnotations;

namespace Kochk.Application.Features.Carts.DTOs;

public class CartDto
{
    [Required]
    public Guid Id { get; set; } = default!;
    public IEnumerable<ProductCartDto> CartProducts { get; set; } = [];
}

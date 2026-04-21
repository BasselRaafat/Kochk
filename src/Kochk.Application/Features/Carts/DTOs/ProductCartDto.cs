using System.ComponentModel.DataAnnotations;

namespace Kochk.Application.Features.Carts.DTOs;

public class ProductCartDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public List<string> PictureUrl { get; set; } = default!;

    [Required]
    public string Category { get; set; } = default!;

    [Required]
    public string Brand { get; set; } = default!;

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}

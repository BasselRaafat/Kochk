using System.ComponentModel.DataAnnotations;

namespace Kochk.Application.Features.Auth.Models;

public class AddressDetailsDto
{
    [Required]
    public string Appartment { get; set; } = default!;

    [Required]
    public string Street { get; set; } = default!;

    [Required]
    public string City { get; set; } = default!;

    [Required]
    public string Country { get; set; } = default!;
}

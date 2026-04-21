namespace Kochk.Application.Features.Auth.Models;

public record UserAddressDetails
{
    public string Appartment { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
}

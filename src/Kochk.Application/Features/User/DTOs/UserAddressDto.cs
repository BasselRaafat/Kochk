namespace Kochk.Application.Features.User.DTOs;

public class UserAddressUpdateRequest
{
    public string Appartment { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    /*public string AppUserId { get; set; }*/
}

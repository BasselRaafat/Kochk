namespace Kochk.Application.Features.Auth.Models;

public class CustomerSignUpRequest
{
    public UserInfoDto UserInfo { get; set; } = default!;
    public AddressDetailsDto Address { get; set; } = default!;
}

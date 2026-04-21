namespace Kochk.Application.Features.Auth.Models
{
    public class VendorSignUpRequest
    {
        public UserInfoDto UserInfo { get; set; } = default!;
        public AddressDetailsDto Address { get; set; } = default!;
        public VendorInfoDto VendorInfo { get; set; } = default!;
    }
}

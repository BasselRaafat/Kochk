using AutoMapper;
using Kochk.Application.Features.Auth.Commands.CustomerSignUp;
using Kochk.Application.Features.Auth.Commands.SignIn;
using Kochk.Application.Features.Auth.Commands.VendorSignUP;
using Kochk.Application.Features.Auth.Models;
using Kochk.Domain.Entities;

namespace Kochk.Application.Common;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<VendorInfo, Vendor>();
        CreateMap<UserInfo, BusinessUser>();
        CreateMap<UserAddressDetails, UserAddress>();

        CreateMap<AddressDetailsDto, UserAddressDetails>();
        CreateMap<UserInfoDto, UserInfo>();
        CreateMap<VendorInfoDto, VendorInfo>();

        CreateMap<CustomerSignUpRequest, CustomerSignUpCommand>();
        CreateMap<VendorSignUpRequest, VendorSignUpCommand>();

        CreateMap<SignInRequest, SignInCommand>();
    }
}

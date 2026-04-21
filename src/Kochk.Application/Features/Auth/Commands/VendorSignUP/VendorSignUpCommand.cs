using Kochk.Application.Common.Models;
using Kochk.Application.Features.Auth.Models;
using MediatR;

namespace Kochk.Application.Features.Auth.Commands.VendorSignUP;

public record VendorSignUpCommand(
    UserInfo UserInfo,
    UserAddressDetails Address,
    VendorInfo VendorInfo
) : IRequest<Result>;

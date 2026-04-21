using Kochk.Application.Common.Models;
using Kochk.Application.Features.Auth.Models;
using MediatR;

namespace Kochk.Application.Features.Auth.Commands.CustomerSignUp;

public record CustomerSignUpCommand(UserInfo UserInfo, UserAddressDetails Address)
    : IRequest<Result>;

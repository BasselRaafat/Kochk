using AutoMapper;
using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Models;
using Kochk.Application.Common.Models;
using Kochk.Application.Features.User.DTOs;
using Kochk.Domain.Entities;
using MediatR;

namespace Kochk.Application.Features.User.Queries;

public record GetUserAddressesQuery(Guid UserId)
    : IRequest<Result<IReadOnlyList<UserAddressUpdateRequest>>>;

internal class GetUserAddressesQueryHandeler(
    IWriteRepository<BusinessUser> userRepo,
    IMapper mapper
// ,ICurrentUserService currentUser
) : IRequestHandler<GetUserAddressesQuery, Result<IReadOnlyList<UserAddressUpdateRequest>>>
{
    public async Task<Result<IReadOnlyList<UserAddressUpdateRequest>>> Handle(
        GetUserAddressesQuery request,
        CancellationToken cancellationToken
    )
    {
        // var userId = currentUser.UserId;

        var user = await userRepo.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Errors.User.NotFound(request.UserId);

        IReadOnlyList<UserAddressUpdateRequest> mappedaddres = mapper.Map<
            IReadOnlyList<UserAddressUpdateRequest>
        >(user!.Addresses.ToList());
        return Result<IReadOnlyList<UserAddressUpdateRequest>>.Success(mappedaddres);
    }
}

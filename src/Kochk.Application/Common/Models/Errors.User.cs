using Kochk.Application.Common.Models;

namespace Kochk.Application.Common.Models;

public static partial class Errors
{
    public static class User
    {
        public static Error NotFound(Guid userId) =>
            new("USER.NOT_FOUND", ErrorType.NotFound, $"User with id {userId} is Not Found");

        public static Error NotFoundAddress(Guid addressId, Guid userId) =>
            new(
                "USER.ADDRESS.NOT_FOUND",
                ErrorType.NotFound,
                $"Address with id {addressId} is Not Found for User {userId}"
            );
    }
}

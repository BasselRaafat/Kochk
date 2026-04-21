namespace Kochk.Application.Common.Models;

public static partial class Errors
{
    public static class Carts
    {
        public static Error NotFound(Guid userId) =>
            new(
                "CART.USER.NOT_FOUND",
                ErrorType.NotFound,
                $"Cart For User With Id {userId} Is Not Found"
            );
    }
}

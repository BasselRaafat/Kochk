namespace Kochk.Application.Common.Models;

public static partial class Errors
{
    public static class Product
    {
        public static Error NotFound(Guid productId) =>
            new(
                "PRODUCT.NOT_FOUND",
                ErrorType.NotFound,
                $"Product with id {productId} is Not Found"
            );
    }
}


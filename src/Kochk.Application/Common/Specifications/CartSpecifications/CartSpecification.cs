using Kochk.Domain.Entities;

namespace Kochk.Application.Common.Specifications.CartSpecifications;

public class CartSpecification : BaseSpecifications<Cart>
{
    public CartSpecification(Guid UserId)
        : base(c => c.UserId == UserId)
    {
        Includes.Add(c => c.CartItems);
        Includes.Add(c => c.User);
    }
}


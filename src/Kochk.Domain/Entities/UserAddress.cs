namespace Kochk.Domain.Entities;

public class UserAddress : BaseEntity
{
    public string Appartment { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    public Guid UserId { get; set; } = default!;

    public BusinessUser User { get; set; } = default!;
}

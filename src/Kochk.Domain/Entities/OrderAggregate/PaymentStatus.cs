using System.Runtime.Serialization;

namespace Kochk.Domain.Entities.OrderAggregate;

public enum PaymentStatus
{
    [EnumMember(Value = "Pending")]
    Pending = 0,

    [EnumMember(Value = "Succeeded")]
    Succeeded = 1,

    [EnumMember(Value = "Failed")]
    Failed = 2,

    [EnumMember(Value = "Refunded")]
    Refunded = 3,
}

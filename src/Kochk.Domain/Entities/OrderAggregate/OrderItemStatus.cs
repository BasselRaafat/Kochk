using System.Runtime.Serialization;

namespace Kochk.Domain.Entities.OrderAggregate;

public enum OrderItemStatus
{
    [EnumMember(Value = "Pending")]
    Pending = 0,

    [EnumMember(Value = "Processing")]
    Processing = 1,

    [EnumMember(Value = "Shipped")]
    Shipped = 2,

    [EnumMember(Value = "Delivered")]
    Delivered = 3,

    [EnumMember(Value = "Returned")]
    Returned = 4,

    [EnumMember(Value = "Canceled")]
    Canceled = 5,
}

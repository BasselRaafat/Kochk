using System.Runtime.Serialization;

namespace Kochk.Domain.Entities.OrderAggregate;

public enum OrderStatus
{
    [EnumMember(Value = "Canceled")]
    Canceled = 0,

    [EnumMember(Value = "Delivered")]
    Delivered = 1,

    [EnumMember(Value = "In Transit")]
    InTransit = 2,

    [EnumMember(Value = "Processing")]
    Processing = 3,

    [EnumMember(Value = "Returned")]
    Returned = 4,
}

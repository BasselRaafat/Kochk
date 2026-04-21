using System.Runtime.Serialization;

namespace Kochk.Domain.Entities;

public enum Role
{
    [EnumMember(Value = "Admin")]
    Admin = 0,

    [EnumMember(Value = "Customer")]
    Customer = 1,

    [EnumMember(Value = "Vendor")]
    Vendor = 2,
}


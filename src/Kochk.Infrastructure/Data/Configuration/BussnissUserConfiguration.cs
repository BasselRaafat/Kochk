using Kochk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kochk.Infrastructure.Data.Configuration;

public class BussnissUserConfiguration : IEntityTypeConfiguration<BusinessUser>
{
    public void Configure(EntityTypeBuilder<BusinessUser> builder)
    {
        builder
            .Property(O => O.Role)
            .HasConversion(
                OrderStatus => OrderStatus.ToString(),
                stringOrder => Enum.Parse<Role>(stringOrder)
            );

        builder
            .HasOne(u => u.Cart)
            .WithOne(a => a.User)
            .HasForeignKey<Cart>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.Orders)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.Role);
    }
}

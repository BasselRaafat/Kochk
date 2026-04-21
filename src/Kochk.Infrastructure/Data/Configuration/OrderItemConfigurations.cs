using Kochk.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kochk.Infrastructure.Data.Configuration;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(OI => OI.Price).HasColumnType("decimal(18,2)");

        builder
            .HasOne(oi => oi.VendorOrder)
            .WithMany(vo => vo.Items)
            .HasForeignKey(oi => oi.VendorOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

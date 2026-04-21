using Kochk.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kochk.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .Property(O => O.OrderStatus)
            .HasConversion(
                orderStatus => orderStatus.ToString(),
                stringOrderStatus => Enum.Parse<OrderStatus>(stringOrderStatus)
            );
        builder
            .Property(O => O.PaymentStatus)
            .HasConversion(
                paymentStatus => paymentStatus.ToString(),
                stringPaymentStatus => Enum.Parse<PaymentStatus>(stringPaymentStatus)
            );

        builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
        builder
            .HasOne(O => O.DeliveryMethod)
            .WithMany()
            .HasForeignKey(o => o.DeliveryMethodId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(o => o.Address)
            .WithMany()
            .HasForeignKey(o => o.AddressId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(o => o.VendorOrders)
            .WithOne(u => u.Order)
            .HasForeignKey(vo => vo.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(o => o.OrderDate);
    }
}

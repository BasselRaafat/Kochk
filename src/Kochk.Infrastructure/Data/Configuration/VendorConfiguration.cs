using Kochk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kochk.Infrastructure.Data.Configuration;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder
            .Property(v => v.Status)
            .HasConversion(
                vendorStatus => vendorStatus.ToString(),
                vendorStatusString => Enum.Parse<VendorStatus>(vendorStatusString)
            );

        builder.Property(v => v.CommissionRate).HasColumnType("decimal(2,2)");

        builder.Property(v => v.Rate).HasColumnType("decimal(2,1)");

        builder
            .HasMany(v => v.Reviews)
            .WithOne(r => r.Vendor)
            .HasForeignKey(r => r.VendorId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(v => v.Products)
            .WithOne(r => r.Vendor)
            .HasForeignKey(p => p.VendorId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(v => v.User)
            .WithOne(u => u.Vendor)
            .HasForeignKey<Vendor>(v => v.Id)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(v => v.Orders)
            .WithOne(vo => vo.Vendor)
            .HasForeignKey(vo => vo.VendorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

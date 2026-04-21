using Kochk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kochk.Infrastructure.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        builder.Property(p => p.Description).IsRequired().HasColumnType("text");

        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(p => p.Discount).IsRequired().HasColumnType("decimal(2,2)");

        builder.HasOne(p => p.Brand).WithMany(b => b.Products).HasForeignKey(p => p.BrandId);

        builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);

        builder
            .HasMany(p => p.PictureUrls)
            .WithOne(ppu => ppu.Product)
            .HasForeignKey(ppu => ppu.PorductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(p => p.CartItems)
            .WithOne(cp => cp.Product)
            .HasForeignKey(cp => cp.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        ;
        builder
            .HasMany(p => p.OrderItems)
            .WithOne(cp => cp.Product)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.Price);
    }
}

using Kochk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kochk.Infrastructure.Data.Configuration;

public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductReview> builder
    )
    {
        builder.Property(pr => pr.Comment).HasColumnType("text");
    }
}

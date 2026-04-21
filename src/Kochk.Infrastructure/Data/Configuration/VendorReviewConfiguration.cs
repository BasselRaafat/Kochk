using Kochk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kochk.Infrastructure.Data.Configuration;

public class VendorReviewConfiguration : IEntityTypeConfiguration<VendorReview>
{
    public void Configure(EntityTypeBuilder<VendorReview> builder)
    {
        builder.Property(vr => vr.Comment).HasColumnType("text");
    }
}


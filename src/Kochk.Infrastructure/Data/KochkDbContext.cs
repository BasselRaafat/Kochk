using System.Reflection;
using Kochk.Domain.Entities;
using Kochk.Domain.Entities.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kochk.Infrastructure.Data;

public class KochkDbContext : DbContext
{
    public DbSet<BusinessUser> Users { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }

    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<VendorReview> VendorReviews { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductPictureUrl> ProductPictureUrls { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    private readonly IPublisher _publisher;

    public KochkDbContext(DbContextOptions<KochkDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.HasDefaultSchema("public");
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entitiesWithEvents = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entitiesWithEvents.SelectMany(e => e.DomainEvents).ToList();

        // Clear domain events
        entitiesWithEvents.ForEach(e => e.ClearDomainEvents());

        // Save changes first
        var result = await base.SaveChangesAsync(cancellationToken);

        // Then publish events
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}

using Kochk.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kochk.Infrastructure.Identity;

public class KochkIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public KochkIdentityDbContext(DbContextOptions<KochkIdentityDbContext> opts)
        : base(opts) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");
    }
}

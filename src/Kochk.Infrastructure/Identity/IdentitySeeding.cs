using Kochk.Domain.Common;
using Kochk.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Kochk.Infrastructure.Identity;

public static class IdentityDataSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser() { Email = "bassel@gmail.com", UserName = "basselraafat" };
            await userManager.CreateAsync(user, "P@33word");
        }
    }

    public static async Task SeedRolesAsync(RoleManager<AppRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            var roles = new List<AppRole>()
            {
                new() { Id = Guid.NewGuid(), Name = Roles.Admin },
                new() { Id = Guid.NewGuid(), Name = Roles.Cusomter },
                new() { Id = Guid.NewGuid(), Name = Roles.Vendor },
            };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}

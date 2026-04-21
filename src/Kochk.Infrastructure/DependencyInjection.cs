using Kochk.Application.Common.Interfaces;
using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.Services;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Application.Features;
using Kochk.Application.Products;
using Kochk.Domain.Entities.Identity;
using Kochk.Infrastructure.Data;
using Kochk.Infrastructure.Identity;
using Kochk.Infrastructure.Repositories;
using Kochk.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Kochk.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<KochkDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    builder.MigrationsHistoryTable("__MigrationsHistory", "public");
                }
            )
        );

        services.AddDbContext<KochkIdentityDbContext>(opts =>
        {
            opts.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    builder.MigrationsHistoryTable("__MigrationsHistory", "Identity");
                }
            );
        });

        services
            .AddIdentity<AppUser, AppRole>(opts =>
            {
                //password config and other too
                opts.Password.RequiredUniqueChars = 2;
            })
            .AddEntityFrameworkStores<KochkIdentityDbContext>();

        services.AddSingleton<IConnectionMultiplexer>(ServiceProvider =>
        {
            var endpoint = configuration["redis:endpoint"];
            var password = configuration["redis:password"];
            var options = new ConfigurationOptions
            {
                EndPoints = { endpoint! },
                Password = password,
                Ssl = true,
                AbortOnConnectFail = false,
                ConnectTimeout = 10000,
            };
            return ConnectionMultiplexer.Connect(options);
        });
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}

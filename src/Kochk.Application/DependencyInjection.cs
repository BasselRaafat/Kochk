using Kochk.Application.Common;
using Kochk.Application.Common.Services;
using Kochk.Application.Features.Auth.Commands.SignIn;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kochk.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = configuration["MediatRLicenseKey"];
            cfg.RegisterServicesFromAssembly(typeof(SignInCommand).Assembly);
        });
        services.AddAutoMapper(config => config.AddProfile<MappingProfiles>());
        services.AddScoped<AppUserServices>();
        return services;
    }
}

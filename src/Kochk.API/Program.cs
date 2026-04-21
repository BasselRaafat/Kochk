using Kochk.API.Extensions;
using Kochk.API.Middlewares;
using Kochk.Application;
using Kochk.Domain.Entities.Identity;
using Kochk.Infrastructure;
using Kochk.Infrastructure.Data;
using Kochk.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services.AddApiService()
    .AddAuth(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration);

//.AddLibraries(builder.Configuration)

var app = builder.Build();

// Data seed

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var dbContext = services.GetRequiredService<KochkDbContext>();
var identityDbContext = services.GetRequiredService<KochkIdentityDbContext>();
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
try
{
    // await dbContext.Database.MigrateAsync();
    //await DataSeed.SeedAsync(dbContext);
    // await identityDbContext.Database.MigrateAsync();
    await IdentityDataSeed.SeedRolesAsync(roleManager);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "Erorr Happened While Updating Database");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

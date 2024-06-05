using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using MultitenantTest.Multitenancy;

namespace MultitenantTest.Infrastructure.Multitenancy;

internal static class Startup
{
    internal static IServiceCollection AddMultitenancy(this IServiceCollection services)
    {
        return services
            .AddDbContext<TenantDbContext>((p, m) =>
            {
                m.UseSqlServer("Server=localhost;Database=MultitenantTest;User Id=sa;Password=Password123!;");
            })
            .AddMultiTenant<FshTenantInfo>()
            .WithClaimStrategy()
            .WithHeaderStrategy()
            .WithEFCoreStore<TenantDbContext, FshTenantInfo>()
            .Services
            .AddScoped<ITenantService, TenantService>();
    }

    internal static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app) =>
        app.UseMultiTenant();
}
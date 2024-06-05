using Finbuckle.MultiTenant.EntityFrameworkCore.Stores.EFCoreStore;
using Microsoft.EntityFrameworkCore;

namespace MultitenantTest.Infrastructure.Multitenancy;

public class TenantDbContext : EFCoreStoreDbContext<FshTenantInfo>
{
    public TenantDbContext(DbContextOptions options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FshTenantInfo>().ToTable("Tenants");
    }
}
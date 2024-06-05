using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultitenantTest.Entities;

namespace MultitenantTest.Infrastructure.Persistence.Context;

public class BaseDbContext : MultiTenantDbContext
{
    public BaseDbContext(IMultiTenantContextAccessor currentTenant, DbContextOptions<BaseDbContext> options)
        : base(currentTenant, options)
    {
    }


    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}
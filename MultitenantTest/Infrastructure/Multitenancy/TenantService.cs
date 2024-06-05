using System.Globalization;
using Finbuckle.MultiTenant.Abstractions;
using MultitenantTest.Dtos;
using MultitenantTest.Multitenancy;

namespace MultitenantTest.Infrastructure.Multitenancy;

internal class TenantService(IMultiTenantStore<FshTenantInfo> tenantStore) : ITenantService
{
    public async Task<List<FshTenantInfo>> GetAllAsync()
    {
        var tenants = await tenantStore.GetAllAsync();
        return tenants.ToList();
    }

    public async Task<bool> ExistsWithIdAsync(string id) =>
        await tenantStore.TryGetAsync(id) is not null;

    public async Task<bool> ExistsWithNameAsync(string name) =>
        (await tenantStore.GetAllAsync()).Any(t => t.Name == name);

    public async Task<FshTenantInfo> GetByIdAsync(string id) =>
        (await GetTenantInfoAsync(id));

    public async Task<string> CreateAsync(CreateTenantRequest request, CancellationToken cancellationToken)
    {
        var tenant = new FshTenantInfo(request.Name, request.Name);
        await tenantStore.TryAddAsync(tenant);
        return tenant.Id;
    }


    private async Task<FshTenantInfo> GetTenantInfoAsync(string id) =>
        await tenantStore.TryGetAsync(id)
        ?? throw new CultureNotFoundException("{0} {1} Not Found.", typeof(FshTenantInfo).Name, id);
}
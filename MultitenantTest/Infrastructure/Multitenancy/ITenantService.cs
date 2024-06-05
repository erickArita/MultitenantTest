using MultitenantTest.Dtos;
using MultitenantTest.Infrastructure.Multitenancy;

namespace MultitenantTest.Multitenancy;

public interface ITenantService
{
    Task<List<FshTenantInfo>> GetAllAsync();
    Task<bool> ExistsWithIdAsync(string id);
    Task<bool> ExistsWithNameAsync(string name);
    Task<FshTenantInfo> GetByIdAsync(string id);
    Task<string> CreateAsync(CreateTenantRequest request, CancellationToken cancellationToken);
}
using Finbuckle.MultiTenant.Abstractions;

namespace MultitenantTest.Infrastructure.Multitenancy;

public class FshTenantInfo : ITenantInfo
{
    public FshTenantInfo()
    {
    }

    public FshTenantInfo(string name, string identifier)
    {
        Id = Guid.NewGuid().ToString();
        Identifier = identifier;
        Name = name;
    }

    /// <summary>
    /// The actual TenantId, which is also used in the TenantId shadow property on the multitenant entities.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// The identifier that is used in headers/routes/querystrings. This is set to the same as Id to avoid confusion.
    /// </summary>
    public string Identifier { get; set; } = default!;

    public string Name { get; set; } = default!;


    string? ITenantInfo.Id
    {
        get => Id;
        set => Id = value ?? throw new InvalidOperationException("Id can't be null.");
    }

    string? ITenantInfo.Identifier
    {
        get => Identifier;
        set => Identifier = value ?? throw new InvalidOperationException("Identifier can't be null.");
    }

    string? ITenantInfo.Name
    {
        get => Name;
        set => Name = value ?? throw new InvalidOperationException("Name can't be null.");
    }
}
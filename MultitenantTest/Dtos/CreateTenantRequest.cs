namespace MultitenantTest.Dtos;

public record CreateTenantRequest
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
}
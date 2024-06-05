using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using MultitenantTest.Dtos;
using MultitenantTest.Infrastructure.Multitenancy;
using MultitenantTest.Infrastructure.Persistence.Context;
using MultitenantTest.Multitenancy;
using MultitenantTest.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddDbContext<TenantDbContext>((options) => options.UseSqlServer(connectionString))
    .AddMultiTenant<FshTenantInfo>()
    .WithHeaderStrategy()
    .WithEFCoreStore<TenantDbContext, FshTenantInfo>()
    .Services
    .AddScoped<ITenantService, TenantService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMultiTenancy();

app.MapGroup("products")
    .MapProductsApi()
    .WithTags("Products");


app.MapGet("tenant", (ITenantService tenantService) =>
{
    var tenants = tenantService.GetAllAsync();
    return tenants;
}).WithName("getAll").WithOpenApi();

app.MapPost("tenant", (ITenantService tenantService, string name) =>
{
    var tenants = tenantService.CreateAsync(new CreateTenantRequest()
    {
        Id = Guid.NewGuid().ToString(),
        Name = name,
    }, default);
    return tenants;
}).WithName("create").WithOpenApi();

await app.RunAsync();
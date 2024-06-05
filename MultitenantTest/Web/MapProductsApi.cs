using Microsoft.EntityFrameworkCore;
using MultitenantTest.Dtos;
using MultitenantTest.Entities;
using MultitenantTest.Infrastructure.Persistence.Context;

namespace MultitenantTest.Web;

public static class MapProductsApiExtension
{
    public static RouteGroupBuilder MapProductsApi(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (BaseDbContext context, CreateProductDto product) =>
        {
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            };
            await context.Products.AddAsync(newProduct);
            await context.SaveChangesAsync();

            return Results.Ok(newProduct);
        }).WithName("createProduct").WithOpenApi();

        group.MapGet("/", async (BaseDbContext context) => Results.Ok(await context.Products.ToListAsync()))
            .WithName("listProducts").WithOpenApi();

        return group;
    }
}
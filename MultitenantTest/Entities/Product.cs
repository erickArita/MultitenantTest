using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;

namespace MultitenantTest.Entities;

[MultiTenant]
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
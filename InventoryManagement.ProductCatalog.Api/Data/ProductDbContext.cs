using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.ProductCatalog.Api.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}
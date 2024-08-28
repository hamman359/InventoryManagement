using InventoryManagement.ProductCatalog.Api.Model.Entities;

namespace InventoryManagement.ProductCatalog.Api.Data.Repositories;

public sealed class ProductRepository : IProductRepository
{
    readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Product product) =>
        _dbContext
            .Set<Product>()
            .Add(product);
}

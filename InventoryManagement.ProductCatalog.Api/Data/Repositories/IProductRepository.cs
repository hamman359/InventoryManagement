using InventoryManagement.ProductCatalog.Api.Model.Entities;

namespace InventoryManagement.ProductCatalog.Api.Data.Repositories;

public interface IProductRepository
{
    void Add(Product product);
}
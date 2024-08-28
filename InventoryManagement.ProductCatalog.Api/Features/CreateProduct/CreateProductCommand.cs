namespace InventoryManagement.ProductCatalog.Api.Features.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Sku,
    string Description)
    : ICommand<Guid>;
using KJWT.SharedKernel.Primatives;

namespace InventoryManagement.ProductCatalog.Api.Model.Entities;

public sealed class Product : AggregateRoot
{
    Product(
        Guid id,
        string name,
        string description,
        string sku)
        : base(id)
    {
        Name = name;
        Description = description;
        Sku = sku;
    }

    Product() { }

    string Name { get; set; }
    string Description { get; set; }
    string Sku { get; set; }

    public static Product Create(
        string name,
        string description,
        string sku)
    {
        return new(
            Ulid.NewUlid().ToGuid(),
            name,
            description,
            sku);
    }

}
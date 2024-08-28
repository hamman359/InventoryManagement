using InventoryManagement.ProductCatalog.Api.Data.Repositories;
using InventoryManagement.ProductCatalog.Api.Model.Entities;

namespace InventoryManagement.ProductCatalog.Api.Features.CreateProduct;

public class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, Guid>
{
    readonly IProductRepository _productRepository;
    readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        Product product = Product.Create(
            request.Name,
            request.Description,
            request.Sku);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(product.Id);
    }
}

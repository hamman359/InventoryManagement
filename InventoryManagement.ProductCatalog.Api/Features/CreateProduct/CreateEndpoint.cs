namespace InventoryManagement.ProductCatalog.Api.Features.CreateProduct;

public class CreateEndpoint
    : EndpointBaseAsync
    .WithRequest<CreateProductCommand>
    .WithActionResult<Guid>
{
    readonly ISender _sender;

    public CreateEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("api/product")]
    [TranslateResultToActionResult()]
    [SwaggerOperation(
        Summary = "Creates a new Product",
        Description = "Creates a new Product",
        OperationId = "Product_Create",
        Tags = ["ProductEndpoint"])]
    public async override Task<ActionResult<Guid>> HandleAsync(
        [FromBody] CreateProductCommand request,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(request, cancellationToken);

        return result.ToActionResult(this);
    }
}

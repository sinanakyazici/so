using SO.Application.Cache;
using SO.Application.Cqrs;
using SO.ProductService.Application.Queries.GetProducts;

namespace SO.ProductService.Application.Commands.CreateProduct;

public class CreateProductCommand : ICommand<bool>
{
    public Guid Id { get; set; }
    public Guid ProductTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string ProductCode { get; set; } = null!;
}

public class CreateProductCommandInvalidateCache : InvalidateCacheRequest<CreateProductCommand>
{
    public override IEnumerable<string> CacheKeys(CreateProductCommand request)
    {
        yield return $"{base.CacheKey<GetProductsQuery>()}";
    }
}
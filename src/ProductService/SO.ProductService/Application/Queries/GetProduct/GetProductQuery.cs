using SO.Application.Cache;
using SO.Application.Cqrs;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Application.Queries.GetProduct;

public class GetProductQuery : IQuery<ProductViewModel>
{
    public Guid ProductId { get; set; }

    public GetProductQuery(Guid productProductId)
    {
        ProductId = productProductId;
    }
}

public class GetProductQueryCache : CacheRequest<GetProductQuery>
{
    public override string CacheKey(GetProductQuery request)
    {
        return $"{base.CacheKey(request)}_{request.ProductId}";
    }
}
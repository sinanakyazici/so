using SO.Application.Cache;
using SO.Application.Cqrs;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Application.Queries.GetProducts;

public class GetProductsQuery : IQuery<IEnumerable<ProductViewModel>>
{
}

public class GetProductsQueryCache : CacheRequest<GetProductsQuery>
{
}
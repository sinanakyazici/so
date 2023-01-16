using SO.Application.Cqrs;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Application.Queries.GetProducts;

public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<ProductViewModel>>
{
    private readonly IProductQueryRepository _productQueryRepository;

    public GetProductsQueryHandler(IProductQueryRepository productQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
    }

    public async Task<IEnumerable<ProductViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productQueryRepository.GetProducts();
    }
}
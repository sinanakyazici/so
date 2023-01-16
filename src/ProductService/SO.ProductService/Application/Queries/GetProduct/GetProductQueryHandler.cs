using SO.Application.Cqrs;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Application.Queries.GetProduct;

public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductViewModel>
{
    private readonly IProductQueryRepository _productQueryRepository;

    public GetProductQueryHandler(IProductQueryRepository productQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
    }

    public async Task<ProductViewModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productQueryRepository.GetProduct(request.ProductId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        return product;
    }
}
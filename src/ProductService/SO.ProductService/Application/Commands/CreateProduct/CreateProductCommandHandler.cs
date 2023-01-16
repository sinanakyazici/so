using AutoMapper;
using SO.Application.Cqrs;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Application.Commands.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, bool>
{
    private readonly IProductCommandRepository _productCommandRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductCommandRepository productCommandRepository, IMapper mapper)
    {
        _productCommandRepository = productCommandRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        await _productCommandRepository.AddAsync(product, cancellationToken);

        return true;
    }
}
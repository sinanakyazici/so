using AutoMapper;
using MediatR;
using SO.Application.Cqrs;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Application.Commands.UpdateProductName;

public class UpdateProductNameCommandHandler : ICommandHandler<UpdateProductNameCommand>
{
    private readonly IProductCommandRepository _productCommandRepository;
    private readonly IMapper _mapper;

    public UpdateProductNameCommandHandler(IProductCommandRepository productCommandRepository, IMapper mapper)
    {
        _productCommandRepository = productCommandRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProductNameCommand request, CancellationToken cancellationToken)
    {
        var product = await _productCommandRepository.GetProduct(request.Id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        _mapper.Map(request, product);
        return Unit.Value;
    }
}
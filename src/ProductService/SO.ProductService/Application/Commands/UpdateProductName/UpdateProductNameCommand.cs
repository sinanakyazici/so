using FluentValidation;
using SO.Application.Cache;
using SO.Application.Cqrs;
using SO.ProductService.Application.Queries.GetProduct;
using SO.ProductService.Application.Queries.GetProducts;

namespace SO.ProductService.Application.Commands.UpdateProductName;

public class UpdateProductNameCommand : ICommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class UpdateProductNameCommandInvalidateCache : InvalidateCacheRequest<UpdateProductNameCommand>
{
    public override IEnumerable<string> CacheKeys(UpdateProductNameCommand request)
    {
        yield return $"{base.CacheKey<GetProductsQuery>()}";
        yield return $"{base.CacheKey<GetProductQuery>()}_{request.Id}";
    }
}


public class UpdateProductNameCommandValidator : AbstractValidator<UpdateProductNameCommand>
{
    public UpdateProductNameCommandValidator()
    {
        RuleFor(product => product.Name).NotNull().NotEmpty().WithMessage("name cannot be empty");
    }
}
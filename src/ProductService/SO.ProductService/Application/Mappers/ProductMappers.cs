using AutoMapper;
using SO.ProductService.Application.Commands.CreateProduct;
using SO.ProductService.Application.Commands.UpdateProductName;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Application.Mappers;

public class ProductMappers : Profile
{
    public ProductMappers()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductNameCommand, Product>();
    }
}
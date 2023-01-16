using MediatR;
using Microsoft.AspNetCore.Mvc;
using SO.ProductService.Application.Commands.CreateProduct;
using SO.ProductService.Application.Queries.GetProducts;
using SO.ProductService.Domain.Product;
using System.Net;
using SO.ProductService.Application.Commands.UpdateProductName;
using SO.ProductService.Application.Queries.GetProduct;

namespace SO.ProductService.Api;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
    {
        var data = await _mediator.Send(new GetProductsQuery());
        return Ok(data);
    }


    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductViewModel>> GetProduct(Guid productId)
    {
        var data = await _mediator.Send(new GetProductQuery(productId));
        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand request)
    {
        var data = await _mediator.Send(request);
        return Ok(data);
    }

    [HttpPatch("{productId:guid}")]
    public async Task<ActionResult> UpdateProductName(Guid productId, [FromBody] UpdateProductNameCommand request)
    {
        request.Id = productId;
        var data = await _mediator.Send(request);
        return Ok(data);
    }
}
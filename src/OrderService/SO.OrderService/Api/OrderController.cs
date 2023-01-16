using MediatR;
using Microsoft.AspNetCore.Mvc;
using SO.OrderService.Application.Commands.CreateOrder;
using SO.OrderService.Application.Queries.GetOrders;
using SO.OrderService.Domain.Order;
using System.Net;
using SO.OrderService.Application.Queries.GetOrder;
using SO.OrderService.Application.Queries.GetOrderSummary;
using SO.Shared.Domain.Order;

namespace SO.OrderService.Api;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrders()
    {
        var data = await _mediator.Send(new GetOrdersQuery());
        return Ok(data);
    }

    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderViewModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderViewModel>> GetOrder(Guid orderId)
    {
        var data = await _mediator.Send(new GetOrderQuery(orderId));
        return Ok(data);
    }

    [HttpGet("summary")]
    [ProducesResponseType(typeof(IEnumerable<OrderSharedModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderSharedModel>>> GetOrderSummary([FromQuery] GetOrderSummaryQuery query)
    {
        var data = await _mediator.Send(query);
        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder([FromBody] CreateOrderCommand request)
    {
        var data = await _mediator.Send(request);
        return Ok(data);
    }
}
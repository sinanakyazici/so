using MediatR;
using Microsoft.AspNetCore.Mvc;
using SO.CustomerService.Application.Commands.CreateCustomer;
using SO.CustomerService.Application.Queries.GetCustomers;
using SO.CustomerService.Domain.Customer;
using System.Net;

namespace SO.CustomerService.Api;

[ApiController]
[Route("api/v1/customers")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomers()
    {
        var data = await _mediator.Send(new GetCustomersQuery());
        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerCommand request)
    {
        var data = await _mediator.Send(request);
        return Ok(data);
    }
}
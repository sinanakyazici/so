using MassTransit;
using MediatR;
using MongoDB.Driver;
using SO.CustomerService.Domain.Customer;
using SO.Shared.Domain.Order;
using SO.Shared.Domain.Order.Events;

namespace SO.CustomerService.Application.Events.IntegrationEvents.OrderCreated;

public class OrderCreatedIntegrationEventHandler : IConsumer<OrderCreatedIntegrationEvent>
{
    private readonly ILogger<OrderCreatedIntegrationEventHandler> _logger;
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly IOrderSharedRepository _orderSharedRepository;

    public OrderCreatedIntegrationEventHandler(ILogger<OrderCreatedIntegrationEventHandler> logger, ICustomerQueryRepository customerQueryRepository, IOrderSharedRepository orderSharedRepository)
    {
        _logger = logger;
        _customerQueryRepository = customerQueryRepository;
        _orderSharedRepository = orderSharedRepository;
    }

    public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
    {
        var customerViewModel = await _customerQueryRepository.GetCustomer(context.Message.CustomerId);
        if (customerViewModel == null)
        {
            _logger.LogError("Customer not found!");
            return;
        }

        var filter = Builders<OrderSharedModel>.Filter.Where(x => x.OrderId == context.Message.OrderId && x.CustomerId == context.Message.CustomerId);
        var update = Builders<OrderSharedModel>.Update
            .Set(x => x.FullName, customerViewModel.FullName)
            .Set(x => x.Email, customerViewModel.Email)
            .Set(x => x.IdentityId, customerViewModel.IdentityId)
            .Set(x => x.FirstName, customerViewModel.FirstName)
            .Set(x => x.LastName, customerViewModel.LastName);
        await _orderSharedRepository.UpdateOrder(filter, update);
    }
}
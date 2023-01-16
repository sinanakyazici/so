using MediatR;
using MongoDB.Driver;
using Polly;
using SO.Shared.Domain.Order;

namespace SO.OrderService.Application.Events.DomainEvents.OrderCreated;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IOrderSharedRepository _orderSharedRepository;

    public OrderCreatedEventHandler(IOrderSharedRepository orderSharedRepository)
    {
        _orderSharedRepository = orderSharedRepository;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderSharedModel>.Filter.Eq(x => x.OrderId, notification.Order.OrderId);
        var orderSharedModels = await _orderSharedRepository.GetOrders(filter);
        if (orderSharedModels.Any())
        {
            await _orderSharedRepository.DeleteOrders(notification.Order.OrderId);
        }

        await _orderSharedRepository.InsertOrder(notification.Order);
    }
}
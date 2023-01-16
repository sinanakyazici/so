using MediatR;
using SO.Shared.Domain.Order;

namespace SO.OrderService.Application.Events.DomainEvents.OrderCreated;

public class OrderCreatedEvent : INotification
{
    public OrderSharedModel Order { get; set; } = null!;
}
using SO.Domain.Events;

namespace SO.Shared.Domain.Order.Events;

public class OrderCreatedIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
}
using MediatR;

namespace SO.CustomerService.Application.Events.DomainEvents.CustomerCreated;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    public CustomerCreatedEventHandler()
    {

    }

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {

    }
}
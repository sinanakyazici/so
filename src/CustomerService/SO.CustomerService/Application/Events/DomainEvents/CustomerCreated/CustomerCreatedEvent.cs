using MediatR;

namespace SO.CustomerService.Application.Events.DomainEvents.CustomerCreated;

public class CustomerCreatedEvent : INotification
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
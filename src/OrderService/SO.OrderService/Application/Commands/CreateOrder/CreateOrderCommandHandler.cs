using AutoMapper;
using SO.Application.Cqrs;
using SO.OrderService.Application.Events.DomainEvents.OrderCreated;
using SO.OrderService.Domain.Order;
using SO.Shared.Domain.Order;
using SO.Shared.Domain.Order.Events;

namespace SO.OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, bool>
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderCommandRepository orderCommandRepository, IMapper mapper)
    {
        _orderCommandRepository = orderCommandRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request);
        order.OrderTotalPrice = order.OrderItems.Sum(x => x.Quantity * x.UnitPrice);
        await _orderCommandRepository.AddAsync(order, cancellationToken);
        var domainEvent = new OrderCreatedEvent
        {
            Order = new OrderSharedModel
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                City = order.Address?.City,
                Country = order.Address?.Country,
                District = order.Address?.District,
                ZipCode = order.Address?.ZipCode,
                Text = order.Address?.Text,
                CreationTime = order.CreationTime,
                Notes = order.Notes,
                OrderTotalPrice = order.OrderTotalPrice,
                OrderItems = order.OrderItems.Select(x => new OrderItemSharedModel
                {
                    ProductId = x.ProductId,
                    OrderId = x.OrderId,
                    OrderItemId = x.Id,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                }).ToArray()
            }
        };

        var integrationEvent = new OrderCreatedIntegrationEvent { OrderId = order.Id, CustomerId = order.CustomerId };
        order.AddDomainEvent(domainEvent);
        order.AddIntegrationEvent(integrationEvent);

        return true;
    }
}
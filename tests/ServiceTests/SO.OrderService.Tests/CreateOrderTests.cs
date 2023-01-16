using AutoMapper;
using AutoMapper.EquivalencyExpression;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SO.OrderService.Application.Commands.CreateOrder;
using SO.OrderService.Application.Events.DomainEvents.OrderCreated;
using SO.OrderService.Application.Mappers;
using SO.OrderService.Domain.Order;
using SO.Shared.Domain.Order;
using SO.Shared.Domain.Order.Events;

namespace SO.OrderService.Tests;

public class CreateOrderTests
{
    private IMapper? _mapper;

    public IMapper Mapper
    {
        get
        {
            if (_mapper == null)
            {
                var c = new MapperConfiguration(cfg =>
                {
                    cfg.AddCollectionMappers();
                    cfg.AddProfile(new OrderMappers());
                });
                _mapper = c.CreateMapper();
            }

            return _mapper;
        }
    }

    [Fact]
    public async Task CreateOrderCommand()
    {
        var mockOrderCommandRepository = new Mock<IOrderCommandRepository>();

        var handler = new CreateOrderCommandHandler(mockOrderCommandRepository.Object, Mapper);
        var command = new CreateOrderCommand
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Address = new CreateOrderCommand.AddressDto
            {
                Country = "test country",
                City = "test city",
                District = "test district",
                Text = "test text",
                ZipCode = "test zipcode"
            },
            Notes = "test notes",
            OrderItems = new List<CreateOrderCommand.OrderItemDto>
            {
                new()
                {
                    Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), ProductName = "test product name", Quantity = 3, UnitPrice = 55
                }
            }
        };
        var expected = Mapper.Map<Order>(command);
        expected.OrderTotalPrice = expected.OrderItems.Sum(x => x.Quantity * x.UnitPrice);
        var integrationEvent = new OrderCreatedIntegrationEvent { OrderId = expected.Id, CustomerId = expected.CustomerId };
        var domainEvent = new OrderCreatedEvent
        {
            Order = new OrderSharedModel
            {
                OrderId = expected.Id,
                CustomerId = expected.CustomerId,
                City = expected.Address?.City,
                Country = expected.Address?.Country,
                District = expected.Address?.District,
                ZipCode = expected.Address?.ZipCode,
                Text = expected.Address?.Text,
                CreationTime = expected.CreationTime,
                Notes = expected.Notes,
                OrderTotalPrice = expected.OrderTotalPrice,
                OrderItems = expected.OrderItems.Select(x => new OrderItemSharedModel
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
        expected.AddDomainEvent(domainEvent);
        expected.AddIntegrationEvent(integrationEvent);

        Order? actual = null;
        mockOrderCommandRepository.Setup(x => x.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
            .Callback(new InvocationAction(i =>
            {
                actual = (Order)i.Arguments[0];
            }));

        await handler.Handle(command, CancellationToken.None);
        var config = new ComparisonConfig();
        config.MembersToIgnore.Add("CreationTime");
        config.MembersToIgnore.Add("Id");
        config.IgnoreProperty<OrderCreatedEvent>(x => x.Order.Id);
        CompareLogic compareLogic = new(config);
        var result = compareLogic.Compare(actual, expected);

        Assert.True(result.AreEqual);
    }
}
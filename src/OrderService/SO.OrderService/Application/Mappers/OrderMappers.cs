using AutoMapper;
using SO.OrderService.Application.Commands.CreateOrder;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Application.Mappers;

public class OrderMappers : Profile
{
    public OrderMappers()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<CreateOrderCommand.AddressDto, Address>();
        CreateMap<CreateOrderCommand.OrderItemDto, OrderItem>();
    }
}
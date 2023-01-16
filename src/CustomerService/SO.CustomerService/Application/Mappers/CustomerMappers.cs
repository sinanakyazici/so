using AutoMapper;
using SO.CustomerService.Application.Commands.CreateCustomer;
using SO.CustomerService.Application.Events.DomainEvents.CustomerCreated;
using SO.CustomerService.Domain.Customer;

namespace SO.CustomerService.Application.Mappers;

public class CustomerMappers : Profile
{
    public CustomerMappers()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<Customer, CustomerCreatedEvent>();
        CreateMap<CreateCustomerCommand.AddressDto, Address>();
    }
}
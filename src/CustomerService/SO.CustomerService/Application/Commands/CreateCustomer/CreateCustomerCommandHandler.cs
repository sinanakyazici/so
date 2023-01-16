using AutoMapper;
using SO.Application.Cqrs;
using SO.CustomerService.Application.Events.DomainEvents.CustomerCreated;
using SO.CustomerService.Domain.Customer;

namespace SO.CustomerService.Application.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, bool>
{
    private readonly ICustomerCommandRepository _customerCommandRepository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerCommandRepository customerCommandRepository, IMapper mapper)
    {
        _customerCommandRepository = customerCommandRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request);
        await _customerCommandRepository.AddAsync(customer, cancellationToken);
        var customerCreated = _mapper.Map<CustomerCreatedEvent>(customer);
        customer.AddDomainEvent(customerCreated);
        return true;
    }
}
using SO.Application.Cqrs;
using SO.CustomerService.Domain.Customer;

namespace SO.CustomerService.Application.Queries.GetCustomers;

public class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, IEnumerable<CustomerViewModel>>
{
    private readonly ICustomerQueryRepository _customerQueryRepository;

    public GetCustomersQueryHandler(ICustomerQueryRepository customerQueryRepository)
    {
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task<IEnumerable<CustomerViewModel>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _customerQueryRepository.GetCustomers();
    }
}
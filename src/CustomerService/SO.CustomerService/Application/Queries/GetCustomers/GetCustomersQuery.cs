using SO.Application.Cqrs;
using SO.CustomerService.Domain.Customer;

namespace SO.CustomerService.Application.Queries.GetCustomers;

public class GetCustomersQuery : IQuery<IEnumerable<CustomerViewModel>>
{
}
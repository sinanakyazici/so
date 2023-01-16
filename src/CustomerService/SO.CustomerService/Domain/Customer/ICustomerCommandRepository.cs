using SO.Domain;

namespace SO.CustomerService.Domain.Customer;

public interface ICustomerCommandRepository : ICommandRepository<Customer>
{
    Task<Customer?> GetCustomer(Guid customerId);
}
using SO.Domain;

namespace SO.CustomerService.Domain.Customer;

public interface ICustomerQueryRepository : IQueryRepository
{
    Task<CustomerViewModel?> GetCustomer(Guid customerId);
    Task<IEnumerable<CustomerViewModel>> GetCustomers();
}
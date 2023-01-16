using Dapper;
using Npgsql;
using SO.Infrastructure.Data.Dapper;
using SO.Infrastructure.Data;
using SO.CustomerService.Domain.Customer;

namespace SO.CustomerService.Infrastructure.Data.QueryRepos;

public class CustomerQueryRepository : DapperRepository, ICustomerQueryRepository
{
    public CustomerQueryRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@now", Helper.DateTimeNow());

        var sql = string.Format(
            "SELECT " +
            // customer
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"identity_id\" AS {2}, " +
            "\"{0}\".\"email\" AS {3}, " +
            "\"{0}\".\"first_name\" AS {4}, " +
            "\"{0}\".\"last_name\" AS {5}, " +
            "\"{0}\".\"nationality\" AS {6}, " +
            "\"{0}\".\"birthdate\" AS {7}, " +
            "\"{0}\".\"phone_number\" AS {8}, " +
            "\"{0}\".\"creator_name\" AS {9}, " +
            // address
            "\"{0}\".\"address_country\" AS {10}, " +
            "\"{0}\".\"address_city\" AS {11}, " +
            "\"{0}\".\"address_district\" AS {12}, " +
            "\"{0}\".\"address_text\" AS {13}, " +
            "\"{0}\".\"address_zip_code\" AS {14} " +
            "FROM \"customer\" \"{0}\" " +
            "WHERE (\"{0}\".valid_for is null or (\"{0}\".valid_for is not null and \"{0}\".valid_for > @now))",
            nameof(Customer),
            nameof(Customer.Id),
            nameof(Customer.IdentityId),
            nameof(Customer.Email),
            nameof(Customer.FirstName),
            nameof(Customer.LastName),
            nameof(Customer.Nationality),
            nameof(Customer.BirthDate),
            nameof(Customer.PhoneNumber),
            nameof(Customer.CreatorName),
            nameof(Customer.Address.Country),
            nameof(Customer.Address.City),
            nameof(Customer.Address.District),
            nameof(Customer.Address.Text),
            nameof(Customer.Address.ZipCode)
            );

        var customerViewModels = Map(connection, sql, parameters);
        return customerViewModels;
    }

    public async Task<CustomerViewModel?> GetCustomer(Guid customerId)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@now", Helper.DateTimeNow());
        parameters.Add("@customerId", customerId);

        var sql = string.Format(
            "SELECT " +
            // customer
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"identity_id\" AS {2}, " +
            "\"{0}\".\"email\" AS {3}, " +
            "\"{0}\".\"first_name\" AS {4}, " +
            "\"{0}\".\"last_name\" AS {5}, " +
            "\"{0}\".\"nationality\" AS {6}, " +
            "\"{0}\".\"birthdate\" AS {7}, " +
            "\"{0}\".\"phone_number\" AS {8}, " +
            "\"{0}\".\"creator_name\" AS {9}, " +
            // address
            "\"{0}\".\"address_country\" AS {10}, " +
            "\"{0}\".\"address_city\" AS {11}, " +
            "\"{0}\".\"address_district\" AS {12}, " +
            "\"{0}\".\"address_text\" AS {13}, " +
            "\"{0}\".\"address_zip_code\" AS {14} " +
            "FROM \"customer\" \"{0}\" " +
            "WHERE \"{0}\".\"id\" = @customerId and (\"{0}\".valid_for is null or (\"{0}\".valid_for is not null and \"{0}\".valid_for > @now))",
            nameof(Customer),
            nameof(Customer.Id),
            nameof(Customer.IdentityId),
            nameof(Customer.Email),
            nameof(Customer.FirstName),
            nameof(Customer.LastName),
            nameof(Customer.Nationality),
            nameof(Customer.BirthDate),
            nameof(Customer.PhoneNumber),
            nameof(Customer.CreatorName),
            nameof(Customer.Address.Country),
            nameof(Customer.Address.City),
            nameof(Customer.Address.District),
            nameof(Customer.Address.Text),
            nameof(Customer.Address.ZipCode)
            );

        var customerViewModel = Map(connection, sql, parameters).SingleOrDefault();
        return customerViewModel;
    }

    private static IEnumerable<CustomerViewModel> Map(NpgsqlConnection connection, string sql, DynamicParameters parameters)
    {
        var lookup = new Dictionary<Guid, CustomerViewModel>();
        var customerViewModels = connection.Query<CustomerViewModel, AddressViewModel, CustomerViewModel>(sql,
            (customer, address) =>
            {
                if (!lookup.TryGetValue(customer.Id, out var c))
                {
                    c = customer;
                    lookup.Add(customer.Id, c);
                }

                if (address != null)
                {
                    c.Address = address;
                }

                return c;
            }, parameters, splitOn: $"{nameof(Customer.Address.Country)}").Distinct();
        return customerViewModels;
    }
}
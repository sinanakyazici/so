using Dapper;
using Npgsql;
using SO.Infrastructure.Data;
using SO.Infrastructure.Data.Dapper;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Infrastructure.Data.QueryRepos;

public class OrderQueryRepository : DapperRepository, IOrderQueryRepository
{
    public OrderQueryRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<OrderViewModel>> GetOrders()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@now", Helper.DateTimeNow());

        var sql = string.Format(
            "SELECT " +
            // order
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"customer_id\" AS {2}, " +
            "\"{0}\".\"order_total_price\" AS {3}, " +
            "\"{0}\".\"notes\" AS {4}, " +
            "\"{0}\".\"creation_time\" AS {10}, " +
            // address
            "\"{0}\".\"address_country\" AS {5}, " +
            "\"{0}\".\"address_city\" AS {6}, " +
            "\"{0}\".\"address_district\" AS {7}, " +
            "\"{0}\".\"address_text\" AS {8}, " +
            "\"{0}\".\"address_zip_code\" AS {9}, " +
            // order_items
            "\"{11}\".\"id\" AS {12}, " +
            "\"{11}\".\"order_id\" AS {13}, " +
            "\"{11}\".\"product_id\" AS {14}, " +
            "\"{11}\".\"product_name\" AS {15}, " +
            "\"{11}\".\"unit_price\" AS {16}, " +
            "\"{11}\".\"quantity\" AS {17} " +
            "FROM \"order\" \"{0}\" " +
            "LEFT JOIN \"order_item\" \"{11}\" ON \"{11}\".\"order_id\" = \"{0}\".\"id\" " +
            "WHERE (\"{0}\".valid_for is null or (\"{0}\".valid_for is not null and \"{0}\".valid_for > @now))",
            nameof(Order),
            nameof(Order.Id),
            nameof(Order.CustomerId),
            nameof(Order.OrderTotalPrice),
            nameof(Order.Notes),
            nameof(Order.Address.Country),
            nameof(Order.Address.City),
            nameof(Order.Address.District),
            nameof(Order.Address.Text),
            nameof(Order.Address.ZipCode),
            nameof(Order.CreationTime),
            nameof(OrderItem),
            nameof(OrderItem) + nameof(OrderItem.Id),
            nameof(OrderItem) + nameof(OrderItem.OrderId),
            nameof(OrderItem) + nameof(OrderItem.ProductId),
            nameof(OrderItem) + nameof(OrderItem.ProductName),
            nameof(OrderItem) + nameof(OrderItem.UnitPrice),
            nameof(OrderItem) + nameof(OrderItem.Quantity)
        );

        var orderViewModels = Map(connection, sql, parameters);
        return orderViewModels;
    }

    public async Task<OrderViewModel?> GetOrder(Guid orderId)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@now", Helper.DateTimeNow());
        parameters.Add("@orderId", orderId);

        var sql = string.Format(
            "SELECT " +
            // order
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"customer_id\" AS {2}, " +
            "\"{0}\".\"order_total_price\" AS {3}, " +
            "\"{0}\".\"notes\" AS {4}, " +
            "\"{0}\".\"creation_time\" AS {10}, " +
            // address
            "\"{0}\".\"address_country\" AS {5}, " +
            "\"{0}\".\"address_city\" AS {6}, " +
            "\"{0}\".\"address_district\" AS {7}, " +
            "\"{0}\".\"address_text\" AS {8}, " +
            "\"{0}\".\"address_zip_code\" AS {9}, " +
            // order_items
            "\"{11}\".\"id\" AS {12}, " +
            "\"{11}\".\"order_id\" AS {13}, " +
            "\"{11}\".\"product_id\" AS {14}, " +
            "\"{11}\".\"product_name\" AS {15}, " +
            "\"{11}\".\"unit_price\" AS {16}, " +
            "\"{11}\".\"quantity\" AS {17} " +
            "FROM \"order\" \"{0}\" " +
            "LEFT JOIN \"order_item\" \"{11}\" ON \"{11}\".\"order_id\" = \"{0}\".\"id\" " +
            "WHERE \"{0}\".\"id\" = @orderId and (\"{0}\".valid_for is null or (\"{0}\".valid_for is not null and \"{0}\".valid_for > @now))",
            nameof(Order),
            nameof(Order.Id),
            nameof(Order.CustomerId),
            nameof(Order.OrderTotalPrice),
            nameof(Order.Notes),
            nameof(Order.Address.Country),
            nameof(Order.Address.City),
            nameof(Order.Address.District),
            nameof(Order.Address.Text),
            nameof(Order.Address.ZipCode),
            nameof(Order.CreationTime),
            nameof(OrderItem),
            nameof(OrderItem) + nameof(OrderItem.Id),
            nameof(OrderItem) + nameof(OrderItem.OrderId),
            nameof(OrderItem) + nameof(OrderItem.ProductId),
            nameof(OrderItem) + nameof(OrderItem.ProductName),
            nameof(OrderItem) + nameof(OrderItem.UnitPrice),
            nameof(OrderItem) + nameof(OrderItem.Quantity)
        );

        var orderViewModel = Map(connection, sql, parameters).SingleOrDefault();
        return orderViewModel;
    }

    private IEnumerable<OrderViewModel> Map(NpgsqlConnection connection, string sql, DynamicParameters parameters)
    {
        MapFor<OrderItemViewModel>(nameof(OrderItem));
        var lookup = new Dictionary<Guid, OrderViewModel>();
        var orderViewModels = connection.Query<OrderViewModel, AddressViewModel, OrderItemViewModel, OrderViewModel>(sql,
            (order, address, orderItem) =>
            {
                if (!lookup.TryGetValue(order.Id, out var c))
                {
                    c = order;
                    lookup.Add(order.Id, c);
                }

                if (address != null)
                {
                    c.Address = address;
                }

                if (orderItem != null && c.OrderItems.All(x => x.Id != orderItem.Id))
                {
                    c.OrderItems.Add(orderItem);
                }

                return c;
            }, parameters, splitOn: $"{nameof(Order.Address.Country)}, {nameof(OrderItem) + nameof(OrderItem.Id)}").Distinct();
        return orderViewModels;
    }

}
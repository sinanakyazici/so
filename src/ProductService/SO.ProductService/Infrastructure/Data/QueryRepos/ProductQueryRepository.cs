using Dapper;
using Npgsql;
using SO.Infrastructure.Data;
using SO.Infrastructure.Data.Dapper;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Infrastructure.Data.QueryRepos;

public class ProductQueryRepository : DapperRepository, IProductQueryRepository
{
    public ProductQueryRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@now", Helper.DateTimeNow());

        var sql = string.Format(
            "SELECT " +
            // product
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"name\" AS {2}, " +
            "\"{0}\".\"product_code\" AS {3}, " +
            "\"{0}\".\"creation_time\" AS {4}, " +
            // product_type
            "\"{0}\".\"product_type_id\" AS {6}, " +
            "\"{5}\".\"name\" AS {7}, " +
            "\"{5}\".\"description\" AS {8} " +
            "FROM \"product\" \"{0}\" " +
            "LEFT JOIN \"product_type\" \"{5}\" ON \"{5}\".\"id\" = \"{0}\".\"product_type_id\" " +
            "WHERE (\"{0}\".valid_for is null or (\"{0}\".valid_for is not null and \"{0}\".valid_for > @now))",
            nameof(Product),
            nameof(Product.Id),
            nameof(Product.Name),
            nameof(Product.ProductCode),
            nameof(Product.CreationTime),
            nameof(ProductType),
            nameof(Product.ProductTypeId),
            nameof(ProductType) + nameof(ProductType.Name),
            nameof(ProductType) + nameof(ProductType.Description)
        );

        var productViewModels = connection.Query<ProductViewModel>(sql, parameters);

        return productViewModels;
    }

    public async Task<ProductViewModel?> GetProduct(Guid productId)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var parameters = new DynamicParameters();
        parameters.Add("@now", Helper.DateTimeNow());
        parameters.Add("@productId", productId);

        var sql = string.Format(
            "SELECT " +
            // product
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"name\" AS {2}, " +
            "\"{0}\".\"product_code\" AS {3}, " +
            "\"{0}\".\"creation_time\" AS {4}, " +
            // product_type
            "\"{0}\".\"product_type_id\" AS {6}, " +
            "\"{5}\".\"name\" AS {7}, " +
            "\"{5}\".\"description\" AS {8} " +
            "FROM \"product\" \"{0}\" " +
            "LEFT JOIN \"product_type\" \"{5}\" ON \"{5}\".\"id\" = \"{0}\".\"product_type_id\" " +
            "WHERE \"{0}\".\"id\" = @productId and (\"{0}\".valid_for is null or (\"{0}\".valid_for is not null and \"{0}\".valid_for > @now))",
            nameof(Product),
            nameof(Product.Id),
            nameof(Product.Name),
            nameof(Product.ProductCode),
            nameof(Product.CreationTime),
            nameof(ProductType),
            nameof(Product.ProductTypeId),
            nameof(ProductType) + nameof(ProductType.Name),
            nameof(ProductType) + nameof(ProductType.Description)
        );

        var productViewModel = connection.Query<ProductViewModel>(sql, parameters).SingleOrDefault();

        return productViewModel;
    }
}
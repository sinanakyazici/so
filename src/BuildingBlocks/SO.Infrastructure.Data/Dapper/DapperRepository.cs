using Dapper;
using Microsoft.Extensions.Configuration;
using SO.Domain;
using System.Reflection;

namespace SO.Infrastructure.Data.Dapper;

public abstract class DapperRepository : IQueryRepository
{
    protected string? ConnectionString;

    protected DapperRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("Query");
    }

    protected void MapFor<T>(string columnPrefix)
    {
        var typeMap = new CustomPropertyTypeMap(typeof(T), (type, name) =>
        {
            if (name.StartsWith(columnPrefix, StringComparison.OrdinalIgnoreCase))
            {
                name = name[columnPrefix.Length..];
            }

            var prop = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null) throw new ArgumentNullException($"{name} prop cannot find in {typeof(T).Name} type.");
            return prop;
        });

        SqlMapper.SetTypeMap(typeof(T), typeMap);
    }
}
using Autofac;
using SO.Infrastructure.Data.EfCore;

namespace SO.ProductService.Infrastructure.Data;

public class ProductContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductContext>().As<BaseDbContext>().InstancePerLifetimeScope();
    }
}

using Autofac;
using SO.Infrastructure.Data.EfCore;

namespace SO.OrderService.Infrastructure.Data;

public class OrderContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OrderContext>().As<BaseDbContext>().InstancePerLifetimeScope();
    }
}

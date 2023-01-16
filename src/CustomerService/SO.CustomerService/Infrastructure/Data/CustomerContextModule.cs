using Autofac;
using SO.Infrastructure.Data.EfCore;

namespace SO.CustomerService.Infrastructure.Data;

public class CustomerContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CustomerContext>().As<BaseDbContext>().InstancePerLifetimeScope();
    }
}
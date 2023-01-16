using Autofac;
using SO.Infrastructure.EventBus.RabbitMq;

namespace SO.Infrastructure.EventBus;

public class EventBusModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RabbitMqConnectionProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}
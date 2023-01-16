using Autofac;
using FluentValidation;
using MediatR;
using SO.Application.Behaviors;
using SO.Application.Cache;
using SO.Application.Events.Domain;
using SO.Application.Events.Integration;
using System.Reflection;

namespace SO.Application.Cqrs;

public class CqrsModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var entryAssembly = Assembly.GetEntryAssembly()!;
        
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
 
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(IValidator<>));
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(INotificationHandler<>));
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(ICacheRequest<>));
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(IInvalidateCacheRequest<>));

        builder.RegisterType<DomainEventsDispatcher>().As<IDomainEventsDispatcher>().InstancePerLifetimeScope();
        builder.RegisterType<IntegrationEventsDispatcher>().As<IIntegrationEventsDispatcher>().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(CachingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(InvalidateCachingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(UnitOfWorkBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        builder.Register<ServiceFactory>(ctx =>
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });
    }
}
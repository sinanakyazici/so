using Autofac;
using Microsoft.AspNetCore.Http;
using SO.Domain;
using SO.Infrastructure.Data.EfCore;
using System.Reflection;

namespace SO.Infrastructure.Data;

public class DataAccessModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var entryAssembly = Assembly.GetEntryAssembly()!;

        builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces();
        builder.RegisterType<EfUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(entryAssembly).AssignableTo<IQueryRepository>().AsImplementedInterfaces();
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(ICommandRepository<>));
    }
}

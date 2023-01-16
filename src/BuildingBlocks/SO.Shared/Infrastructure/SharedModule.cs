using Autofac;
using Autofac.Core;
using MongoDB.Driver;
using SO.Domain;
using SO.Infrastructure.Data.Mongo;
using System.Reflection;

namespace SO.Shared.Infrastructure
{
    public class SharedModule : Autofac.Module
    {
        private readonly MongoConfig _mongoConfig;

        public SharedModule(MongoConfig mongoConfig)
        {
            _mongoConfig = mongoConfig;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var entryAssembly = Assembly.GetExecutingAssembly()!;

            builder.RegisterType<MongoClient>().As<IMongoClient>().WithParameters(new List<Parameter> { new NamedParameter("connectionString", _mongoConfig.ConnectionString) });
            builder.RegisterAssemblyTypes(entryAssembly).AssignableTo<ISharedRepository>().AsImplementedInterfaces();
        }
    }
}
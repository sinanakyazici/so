using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using SO.Application.Cache;
using SO.Application.Cqrs;
using SO.Application.Mappers;
using SO.Application.Middleware;
using SO.CustomerService.Infrastructure.Data;
using SO.Infrastructure.Data;
using SO.Infrastructure.Data.Mongo;
using SO.Infrastructure.EventBus;
using SO.Infrastructure.EventBus.MassTransit;
using SO.Infrastructure.EventBus.RabbitMq;
using SO.Infrastructure.Logger;
using SO.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
.AddEnvironmentVariables();

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection(nameof(RabbitMqConfig)));
builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection(nameof(MongoConfig)));
builder.Services.Configure<CacheConfig>(builder.Configuration.GetSection(nameof(CacheConfig)));

var rabbitMqConfig = builder.Configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();
var mongoConfig = builder.Configuration.GetSection(nameof(MongoConfig)).Get<MongoConfig>();
var cacheConfig = builder.Configuration.GetSection(nameof(CacheConfig)).Get<CacheConfig>();

builder.Services.AddMassTransitForRabbitMq(rabbitMqConfig);
builder.Services.AddCustomCaching(cacheConfig);

builder.Host.UseSerilog(SerilogExtension.CreateLogger(builder.Configuration));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutoMapperModule());
    containerBuilder.RegisterModule(new DataAccessModule());
    containerBuilder.RegisterModule(new SharedModule(mongoConfig));
    containerBuilder.RegisterModule(new EventBusModule());
    containerBuilder.RegisterModule(new CqrsModule());
    containerBuilder.RegisterModule(new CustomerContextModule());
});

var app = builder.Build();

//Configure Context
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();

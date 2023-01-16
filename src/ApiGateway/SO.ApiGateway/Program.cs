using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using SO.Infrastructure.Logger;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile("ocelot.json", false, true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Host.UseSerilog(SerilogExtension.CreateLogger(builder.Configuration));

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

await app.UseOcelot();
app.Run();
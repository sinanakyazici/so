using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace SO.Infrastructure.Logger;

public static class SerilogExtension
{
    public static Serilog.Core.Logger CreateLogger(ConfigurationManager builderConfiguration)
    {
        var elasticsearchUri = builderConfiguration["Elasticsearch:Uri"];
        var indexFormat = builderConfiguration["Elasticsearch:IndexFormat"];
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
#if DEBUG
            .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
            .MinimumLevel.Override("System", LogEventLevel.Verbose)
#else
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
#endif
            .WriteTo.Console()
            .WriteTo.File(Path.Combine("Logs", "log-.txt"), retainedFileCountLimit: 3, rollingInterval: RollingInterval.Day);

        if (!string.IsNullOrWhiteSpace(indexFormat) && !string.IsNullOrWhiteSpace(elasticsearchUri) && Uri.TryCreate(elasticsearchUri, UriKind.Absolute, out var uri))
        {
            logger.WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(
                        uri)
                    {
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        AutoRegisterTemplate = true,
                        IndexFormat = indexFormat,
                        TemplateName = "serilog-events-template"
                    });
        }

        return logger.CreateLogger();
    }
}
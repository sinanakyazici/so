using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace SO.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await LoggingProcess(request, next);
    }

    private async Task<TResponse> LoggingProcess(TRequest request, RequestHandlerDelegate<TResponse> next)
    {
        var requestBody = string.Empty;
#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            requestBody = JsonSerializer.Serialize(request);
        }
        catch
        {
            // ignored
        }
#pragma warning restore CA1031 // Do not catch general exception types

        var hasError = false;
        var sw = new Stopwatch();
        try
        {
            sw.Start();
            var response = await next();
            sw.Stop();

            return response;
        }
        catch
        {
            hasError = true;
            sw.Stop();
            throw;
        }
        finally
        {
            var t = new
            {
                RequestBody = requestBody,
                RequestName = typeof(TRequest).FullName,
                Duration = sw.ElapsedMilliseconds,
                HasError = hasError
            };
            var json = JsonSerializer.Serialize(t);
#pragma warning disable CA2254
            _logger.LogInformation(json);
#pragma warning restore CA2254
        }
    }
}

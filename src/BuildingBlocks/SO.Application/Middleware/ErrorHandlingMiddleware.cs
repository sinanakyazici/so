using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SO.Domain;

namespace SO.Application.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var msg = ex.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList();
                _logger.LogError(ex, nameof(ValidationException));
                var json = JsonSerializer.Serialize(msg);
                await context.Response.WriteAsync(json);
            }
            catch (BaseException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;
                var msg = AggregateInnerMessages(ex);
                _logger.LogError(ex, nameof(BaseException));
                var json = JsonSerializer.Serialize(new { ErrorMessage = msg });
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var msg = AggregateInnerMessages(ex);
                _logger.LogError(ex, nameof(Exception));
                var json = JsonSerializer.Serialize(new { ErrorMessage = msg });
                await context.Response.WriteAsync(json);
            }
            finally
            {
#pragma warning disable CA2254
                _logger.LogInformation($"Request {context.Request?.Method} {context.Request?.Path.Value} => {context.Response?.StatusCode}");
#pragma warning restore CA2254
            }
        }

        private static string AggregateInnerMessages(Exception ex)
        {
            var temp = ex;
            while (true)
            {
                if (temp?.InnerException == null) break;
                temp = temp.InnerException;
            }

            return temp == null ? string.Empty : temp.Message;
        }
    }

}
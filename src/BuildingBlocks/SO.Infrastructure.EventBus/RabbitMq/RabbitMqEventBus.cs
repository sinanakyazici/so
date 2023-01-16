using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client.Exceptions;
using SO.Domain.Events;
using SO.Domain.Events.Bus;
using System.Net.Sockets;
using System.Text.Json;

namespace SO.Infrastructure.EventBus.RabbitMq;

public class RabbitMqEventBus : IEventBus, IDisposable
{
    private readonly IRabbitMqConnectionProvider _rabbitMqConnectionProvider;
    private readonly ILogger<RabbitMqEventBus> _logger;
    private readonly RabbitMqConfig _options;

    public RabbitMqEventBus(IRabbitMqConnectionProvider rabbitMqConnectionProvider, ILogger<RabbitMqEventBus> logger, IOptions<RabbitMqConfig> options)
    {
        _rabbitMqConnectionProvider = rabbitMqConnectionProvider ?? throw new ArgumentNullException(nameof(rabbitMqConnectionProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options.Value;
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        _rabbitMqConnectionProvider.TryConnect();

        var policy = Policy.Handle<BrokerUnreachableException>().Or<SocketException>().WaitAndRetry(_options.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (ex, time) =>
            {
                _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
            });

        var eventName = @event.GetType().Name;

        _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

        using var channel = _rabbitMqConnectionProvider.CreateModel();

        _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

        channel.ExchangeDeclare(exchange: _options.ExchangeName, type: "direct", durable: true, autoDelete: false, arguments: null);

        var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // persistent
            _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);
            channel.BasicPublish(exchange: _options.ExchangeName, routingKey: eventName, mandatory: true, basicProperties: properties, body: body);
        });
    }

    public void Subscribe<TEvent, TEventHandler>() where TEvent : IntegrationEvent where TEventHandler : IIntegrationEventHandler<TEvent>
    {
        throw new NotImplementedException();
    }

    public void Unsubscribe<TEvent, TEventHandler>() where TEvent : IntegrationEvent where TEventHandler : IIntegrationEventHandler<TEvent>
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _rabbitMqConnectionProvider.Dispose();
        GC.SuppressFinalize(this);
    }
}
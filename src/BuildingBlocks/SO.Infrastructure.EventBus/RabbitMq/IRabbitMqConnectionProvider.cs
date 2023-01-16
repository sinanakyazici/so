using RabbitMQ.Client;

namespace SO.Infrastructure.EventBus.RabbitMq;

public interface IRabbitMqConnectionProvider : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}
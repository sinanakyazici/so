namespace SO.Infrastructure.EventBus.RabbitMq;

public class RabbitMqConfig
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Hostname { get; set; } = null!;
    public string VirtualHost { get; set; } = null!;
    public int Port { get; set; }
    public int RetryCount { get; set; }
    public string ExchangeName { get; set; } = null!;
}
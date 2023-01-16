namespace SO.Domain.Events.Bus;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;

    void AddSubscription<TEvent, TEventHandler>()
        where TEvent : IntegrationEvent
        where TEventHandler : IIntegrationEventHandler<TEvent>;

    void RemoveSubscription<TEvent, TEventHandler>()
        where TEventHandler : IIntegrationEventHandler<TEvent>
        where TEvent : IntegrationEvent;

    bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
    bool HasSubscriptionsForEvent(string eventName);
    Type GetEventTypeByName(string eventName);
    Type GetHandlerTypeByName(string eventName);

    IEnumerable<string> GetEvents();

    void Clear();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    string GetEventKey<T>();
}

namespace SO.Domain.Events;

public class SubscriptionInfo
{
    public bool IsDynamic { get; }
    public Type HandlerType { get; }
    public Type HandlerResponseType { get; }

    private SubscriptionInfo(bool isDynamic, Type handlerType, Type handlerResponseType)
    {
        IsDynamic = isDynamic;
        HandlerType = handlerType;
        HandlerResponseType = handlerResponseType;
    }

    public static SubscriptionInfo Dynamic(Type handlerType, Type handlerResponseType)
    {
        return new SubscriptionInfo(true, handlerType, handlerResponseType);
    }
    public static SubscriptionInfo Typed(Type handlerType, Type handlerResponseType)
    {
        return new SubscriptionInfo(false, handlerType, handlerResponseType);
    }
}

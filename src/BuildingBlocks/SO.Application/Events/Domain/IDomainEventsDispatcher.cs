namespace SO.Application.Events.Domain
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
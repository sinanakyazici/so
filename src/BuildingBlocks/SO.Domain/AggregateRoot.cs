using MediatR;
using SO.Domain.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Domain
{
    public interface IAggregateRoot : IEntity
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        IReadOnlyCollection<IntegrationEvent> IntegrationEvents { get; }
        void AddDomainEvent(INotification eventItem);
        void AddIntegrationEvent(IntegrationEvent eventItem);
        void RemoveDomainEvent(INotification eventItem);
        void RemoveIntegrationEvent(IntegrationEvent eventItem);
        void ClearDomainEvents();
        void ClearIntegrationEvents();
    }

    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        private readonly List<INotification> _domainEvents;
        private readonly List<IntegrationEvent> _integrationEvents;

        [NotMapped]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
        [NotMapped]
        public IReadOnlyCollection<IntegrationEvent> IntegrationEvents => _integrationEvents.AsReadOnly();

        protected AggregateRoot()
        {
            _domainEvents = new List<INotification>();
            _integrationEvents = new List<IntegrationEvent>();
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void AddIntegrationEvent(IntegrationEvent eventItem)
        {
            _integrationEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void RemoveIntegrationEvent(IntegrationEvent eventItem)
        {
            _integrationEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public void ClearIntegrationEvents()
        {
            _integrationEvents?.Clear();
        }
    }
}
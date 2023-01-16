using MediatR;
using SO.Domain;

namespace SO.Application.Events.Domain;

public class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DomainEventsDispatcher(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task DispatchEventsAsync()
    {
        var domainEntities = _unitOfWork.GetEntities()
            .Where(e => e.GetType().GetInterfaces().Contains(typeof(IAggregateRoot)))
            .Select(x => (IAggregateRoot)x)
            .Where(x => x.DomainEvents.Any()).ToList();

        var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();

        domainEntities.ForEach(entity => entity.ClearDomainEvents());
        var tasks = domainEvents.Select(async (domainEvent) => { await _mediator.Publish(domainEvent); });

        await Task.WhenAll(tasks);
    }
}
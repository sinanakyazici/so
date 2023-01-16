using MassTransit;
using SO.Domain;

namespace SO.Application.Events.Integration;

public class IntegrationEventsDispatcher : IIntegrationEventsDispatcher
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;

    public IntegrationEventsDispatcher(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }

    public void DispatchEventsAsync()
    {
        var integrationEntities = _unitOfWork.GetEntities()
            .Where(e => e.GetType().GetInterfaces().Contains(typeof(IAggregateRoot)))
            .Select(x => (IAggregateRoot)x)
            .Where(x => x.IntegrationEvents.Any()).ToList();

        var integrationEvents = integrationEntities.SelectMany(x => x.IntegrationEvents).ToList();
        integrationEntities.ForEach(entity => entity.ClearIntegrationEvents());

        integrationEvents.ForEach(x =>
        {
            var type = x.GetType();
            _publishEndpoint.Publish(x, type);
        });
    }
}

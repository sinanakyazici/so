using MediatR;
using SO.Application.Events.Domain;
using SO.Application.Events.Integration;
using SO.Domain;

namespace SO.Application.Behaviors
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly IIntegrationEventsDispatcher _integrationEventsDispatcher;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork, IDomainEventsDispatcher domainEventsDispatcher, IIntegrationEventsDispatcher integrationEventsDispatcher)
        {
            _unitOfWork = unitOfWork;
            _domainEventsDispatcher = domainEventsDispatcher;
            _integrationEventsDispatcher = integrationEventsDispatcher;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = await next();
            await _domainEventsDispatcher.DispatchEventsAsync();
            await _unitOfWork.CommitAsync(cancellationToken); 
            _integrationEventsDispatcher.DispatchEventsAsync();
            return result;
        }
    }
}
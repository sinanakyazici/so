using FluentValidation;
using MediatR;

namespace SO.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator != null)
            {
                var failures = (await _validator.ValidateAsync(request, cancellationToken)).Errors;
                if (failures.Any())
                {
                    throw new ValidationException("Validation exception", failures);
                }
            }

            return await next();
        }
    }
}
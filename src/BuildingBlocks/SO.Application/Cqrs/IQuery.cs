using MediatR;

namespace SO.Application.Cqrs
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
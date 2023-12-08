using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Mediator;

public class CommandHandlerAdapter<TRequest, TResponse> : IRequestHandler<RequestAdapter<TRequest, TResponse>, TResponse>
{
	public Task<TResponse> Handle(RequestAdapter<TRequest, TResponse> request, CancellationToken cancellationToken)
	{
		return null;
	}
}

public class CommandUnitHandlerAdapter<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
{
	public Task Handle(TRequest request, CancellationToken cancellationToken)
	{
		return Unit.Task;
	}
}
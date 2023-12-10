using Marketplace.Application.Infrastructure.Mediator;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Mediator;

internal class MediatRAdapter : IApplicationMediator
{
	private readonly IMediator _mediator;
    public MediatRAdapter(IMediator mediator)
    {
		_mediator = mediator;
    }

	public Task<TResponse> Query<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IQuery<TResponse>
	{
		var requestAdapter = new RequestAdapter<TRequest, TResponse>(request);

		var result = _mediator.Send(requestAdapter, cancellationToken);

		return result;
	}

	public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommandResponse<TResponse>
	{
		var requestAdapter = new RequestAdapter<TRequest, TResponse>(request);

		return _mediator.Send(requestAdapter, cancellationToken);
	}

	public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommand
	{
		RequestAdapter<TRequest, Unit> requestAdapter = new RequestAdapter<TRequest, Unit>(request);

		return _mediator.Send(requestAdapter, cancellationToken);
	}
}
using Framework.Application.Mediator;
using Framework.Query.Mediator;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Mediator.MediatR;

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

	public Task<TResponse> Query<TResponse>(IQuery<TResponse> request, CancellationToken cancellationToken = default)
	{
		Type repositoryType = typeof(RequestAdapter<,>).MakeGenericType(request.GetType(), typeof(TResponse));
		object? requestAdapter = Activator.CreateInstance(repositoryType, request)!;

		if (requestAdapter == null)
		{
			throw new ArgumentNullException(nameof(requestAdapter));
		}

		var taskResult = _mediator.Send((IRequest<TResponse>)requestAdapter, cancellationToken);

		return taskResult;
	}

	public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommandResponse<TResponse>
	{
		RequestAdapter<TRequest, TResponse> requestAdapter = new RequestAdapter<TRequest, TResponse>(request);

		return _mediator.Send(requestAdapter, cancellationToken);
	}

	public Task<TResponse> Send<TResponse>(ICommandResponse<TResponse> request, CancellationToken cancellationToken = default)
	{
		Type repositoryType = typeof(RequestAdapter<,>).MakeGenericType(request.GetType(), typeof(TResponse));
		object? requestAdapter = Activator.CreateInstance(repositoryType, request)!;

		if (requestAdapter == null)
		{
			throw new ArgumentNullException(nameof(requestAdapter));
		}

		var taskResult = _mediator.Send((IRequest<TResponse>)requestAdapter, cancellationToken);

		return taskResult;
	}

	public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommand
	{
		RequestUnitAdapter<TRequest> requestAdapter = new RequestUnitAdapter<TRequest>(request);

		return _mediator.Send(requestAdapter, cancellationToken);
	}
}
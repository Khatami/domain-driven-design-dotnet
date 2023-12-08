using Marketplace.Application.Infrastructure.Mediator;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Mediator;

public class CommandHandlerAdapter<TRequest, TResponse> : IRequestHandler<RequestAdapter<TRequest, TResponse>, TResponse> where TRequest : ICommandResponse<TResponse>
{
	private readonly ICommandHandler<TRequest, TResponse> _commandHandler;
	public CommandHandlerAdapter(ICommandHandler<TRequest, TResponse> commandHandler)
	{
		_commandHandler = commandHandler;
	}

	public Task<TResponse> Handle(RequestAdapter<TRequest, TResponse> request, CancellationToken cancellationToken)
	{
		return _commandHandler.Handle(request.Value, cancellationToken);
	}
}

public class CommandUnitHandlerAdapter<TRequest> : IRequestHandler<RequestAdapter<TRequest, Unit>, Unit> where TRequest : ICommand
{
	private readonly ICommandHandler<TRequest> _commandHandler;
	public CommandUnitHandlerAdapter(ICommandHandler<TRequest> commandHandler)
	{
		_commandHandler = commandHandler;
	}

	public Task<Unit> Handle(RequestAdapter<TRequest, Unit> request, CancellationToken cancellationToken)
	{
		_commandHandler.Handle(request.Value, cancellationToken);

		return Unit.Task;
	}
}
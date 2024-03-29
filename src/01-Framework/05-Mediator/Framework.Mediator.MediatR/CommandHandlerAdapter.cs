using Framework.Application.Mediator;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Mediator.MediatR;

internal class CommandHandlerAdapter<TRequest, TResponse> : IRequestHandler<RequestAdapter<TRequest, TResponse>, TResponse> where TRequest : ICommandResponse<TResponse>
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

internal class CommandUnitHandlerAdapter<TRequest> : IRequestHandler<RequestUnitAdapter<TRequest>> where TRequest : ICommand
{
	private readonly ICommandHandler<TRequest> _commandHandler;
	public CommandUnitHandlerAdapter(ICommandHandler<TRequest> commandHandler)
	{
		_commandHandler = commandHandler;
	}

	public Task Handle(RequestUnitAdapter<TRequest> request, CancellationToken cancellationToken)
	{
		return _commandHandler.Handle(request.Value, cancellationToken);
	}
}
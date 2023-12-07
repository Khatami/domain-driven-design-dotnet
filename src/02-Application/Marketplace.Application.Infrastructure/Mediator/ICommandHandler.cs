using MediatR;

namespace Marketplace.Application.Infrastructure.Mediator
{
	public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
	{
	}

	public interface ICommandHandler<in TCommand, TReturnValue> :
		IRequestHandler<TCommand, TReturnValue> where TCommand : MediatR.IRequest<TReturnValue>
	{
	}
}

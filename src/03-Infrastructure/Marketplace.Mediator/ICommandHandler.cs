namespace Marketplace.Mediator;

public interface ICommandHandler<in TCommand> : MediatR.IRequestHandler<TCommand> where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TReturnValue> :
	MediatR.IRequestHandler<TCommand, TReturnValue> where TCommand : ICommand<TReturnValue>
{
}

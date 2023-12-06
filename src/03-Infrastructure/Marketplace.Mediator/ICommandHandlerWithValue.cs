namespace Marketplace.Mediator;

public interface ICommandHandlerWithValue<in TCommand, TReturnValue> :
	MediatR.IRequestHandler<TCommand, TReturnValue> where TCommand : ICommandWithValue<TReturnValue>
{
}

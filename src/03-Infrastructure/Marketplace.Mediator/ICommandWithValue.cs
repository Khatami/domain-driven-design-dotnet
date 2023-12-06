namespace Marketplace.Mediator;

public interface ICommandWithValue<TReturnValue> : ICommand, MediatR.IRequest<TReturnValue>
{
}

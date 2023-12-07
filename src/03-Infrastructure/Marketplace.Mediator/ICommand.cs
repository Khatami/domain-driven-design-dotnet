namespace Marketplace.Mediator;

public interface ICommand : MediatR.IRequest
{
}

public interface ICommand<TReturnValue> : ICommand, MediatR.IRequest<TReturnValue>
{
}

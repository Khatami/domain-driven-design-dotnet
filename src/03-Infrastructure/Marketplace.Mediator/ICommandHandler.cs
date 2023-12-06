namespace Marketplace.Mediator;

public interface ICommandHandler<in TCommand> : MediatR.IRequestHandler<TCommand> where TCommand : ICommand
{
}

namespace Marketplace.Application.Infrastructure.Mediator
{
	public interface ICommandHandler<in TCommand> where TCommand : ICommand
	{
	}

	public interface ICommandHandler<in TCommand, TReturnValue>
	{
	}
}

namespace Marketplace.Application.SeedWork.Mediator
{
	public interface ICommandHandler<in TCommand> where TCommand : ICommand
	{
		Task Handle(TCommand request, CancellationToken cancellationToken);
	}

	public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommandResponse<TResponse>
	{
		Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
	}
}

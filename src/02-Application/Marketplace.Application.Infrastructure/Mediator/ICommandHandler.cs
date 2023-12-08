namespace Marketplace.Application.Infrastructure.Mediator
{
	public interface ICommandHandler<in TCommand>
	{
		Task Handle(TCommand request, CancellationToken cancellationToken);
	}

	public interface ICommandHandler<in TCommand, TResponse>
	{
		Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
	}
}

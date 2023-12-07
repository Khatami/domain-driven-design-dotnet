namespace Marketplace.Application.Contracts.Infrastructure
{
	public interface ICommandHandler<in TRequest> where TRequest : IRequest
	{
		Task Handle(TRequest request, CancellationToken cancellationToken);
	}
}

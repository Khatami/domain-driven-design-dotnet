namespace Marketplace.Application.Infrastructure.Mediator;

public interface IMediator
{
	Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default);
}

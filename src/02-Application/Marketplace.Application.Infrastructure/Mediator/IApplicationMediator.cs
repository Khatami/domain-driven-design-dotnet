namespace Marketplace.Application.Infrastructure.Mediator;

public interface IApplicationMediator
{
	Task Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default);

	Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default);

	Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default);
}

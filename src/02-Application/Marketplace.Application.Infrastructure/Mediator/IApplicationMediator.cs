namespace Marketplace.Application.Infrastructure.Mediator;

public interface IApplicationMediator
{
	Task Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommandResponse<TResponse>;

	Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommand;

	Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default);
}

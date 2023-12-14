namespace Marketplace.Application.Infrastructure.Mediator;

public interface IApplicationMediator
{
	Task<TResponse> Query<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IQuery<TResponse>;
	
	Task<TResponse> Query<TResponse>(IQuery<TResponse> request, CancellationToken cancellationToken = default);

	Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommandResponse<TResponse>;

	Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : ICommand;

	Task<TResponse> Send<TResponse>(ICommandResponse<TResponse> request, CancellationToken cancellationToken = default);
}

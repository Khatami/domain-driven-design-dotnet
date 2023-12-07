namespace Marketplace.Application.Contracts.Infrastructure;

public interface IMediator
{
	Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default);
}

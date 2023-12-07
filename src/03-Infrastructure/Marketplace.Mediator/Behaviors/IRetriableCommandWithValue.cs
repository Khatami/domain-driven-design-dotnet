using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Mediator.Behaviors;

public interface IRetriableCommandWithValue<TRequest, TResponse> where TRequest : IRequest
{
	int RetryAttempts => 1;

	int RetryDelay => 250;

	bool RetryWithExponentialBackoff => false;

	int ExceptionsAllowedBeforeCircuitTrip => 1;
}

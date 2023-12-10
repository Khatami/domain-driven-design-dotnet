using Marketplace.Application.Infrastructure.Mediator;

namespace Marketplace.Mediator.Behaviors;

internal interface IRetriableCommandWithValue<TRequest, TResponse> where TRequest : ICommand
{
	int RetryAttempts => 1;

	int RetryDelay => 250;

	bool RetryWithExponentialBackoff => false;

	int ExceptionsAllowedBeforeCircuitTrip => 1;
}

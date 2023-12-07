namespace Marketplace.Mediator.Behaviors;

public interface IRetriableCommandWithValue<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
	internal int RetryAttempts => 1;

	internal int RetryDelay => 250;

	internal bool RetryWithExponentialBackoff => false;

	internal int ExceptionsAllowedBeforeCircuitTrip => 1;
}

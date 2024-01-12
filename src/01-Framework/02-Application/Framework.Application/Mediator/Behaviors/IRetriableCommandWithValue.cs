namespace Framework.Application.Mediator.Behaviors;

public interface IRetriableCommandWithValue<TRequest, TResponse> where TRequest : ICommand
{
	int RetryAttempts => 1;

	int RetryDelay => 250;

	bool RetryWithExponentialBackoff => false;

	int ExceptionsAllowedBeforeCircuitTrip => 1;
}

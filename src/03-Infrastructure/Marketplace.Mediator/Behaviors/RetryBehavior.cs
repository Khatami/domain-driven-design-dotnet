﻿using Polly;
using System.Linq;

namespace Marketplace.Mediator.Behaviors;

public class RetryBehavior<TRequest, TResponse> : MediatR.IPipelineBehavior<TRequest, TResponse> where TRequest : ICommandWithValue<TResponse>
{
	private readonly System.Collections.Generic.IList<IRetriableCommandWithValue<TRequest, TResponse>> _retryHandlers;

	public RetryBehavior(System.Collections.Generic.IList<IRetriableCommandWithValue<TRequest, TResponse>> retryHandlers)
	{
		_retryHandlers = retryHandlers;
	}

	public async
		System.Threading.Tasks.Task<TResponse>
		Handle
		(TRequest request, MediatR.RequestHandlerDelegate<TResponse> next,
		System.Threading.CancellationToken cancellationToken)
	{
		var retryHandler = _retryHandlers.FirstOrDefault();

		if (retryHandler == null)
		{
			// No retry handler found, continue through pipeline
			return await next();
		}

		var circuitBreaker = Polly.Policy<TResponse>
			.Handle<System.Exception>()
			.CircuitBreakerAsync(retryHandler.ExceptionsAllowedBeforeCircuitTrip, System.TimeSpan.FromMilliseconds(5000),
			(exception, things) => { }, () => { });

		var retryPolicy =
			Polly.Policy<TResponse>
			.Handle<System.Exception>()
			.WaitAndRetryAsync(retryHandler.RetryAttempts, retryAttempt =>
			{
				var retryDelay = retryHandler.RetryWithExponentialBackoff
					? System.TimeSpan.FromMilliseconds(System.Math.Pow(2, retryAttempt) * retryHandler.RetryDelay)
					: System.TimeSpan.FromMilliseconds(retryHandler.RetryDelay);

				return retryDelay;
			});

		var response = await retryPolicy.ExecuteAsync(async () => await next());

		return response;
	}
}
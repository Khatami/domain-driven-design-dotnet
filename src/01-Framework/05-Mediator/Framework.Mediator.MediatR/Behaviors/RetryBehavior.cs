﻿using Framework.Application.Mediator;
using Framework.Application.Mediator.Behaviors;
using MediatR;
using Polly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Mediator.MediatR.Behaviors;

internal class RetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand
{
	private readonly IList<IRetriableCommandWithValue<TRequest, TResponse>> _retryHandlers;

	public RetryBehavior(IList<IRetriableCommandWithValue<TRequest, TResponse>> retryHandlers)
	{
		_retryHandlers = retryHandlers;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var retryHandler = _retryHandlers.FirstOrDefault();

		if (retryHandler == null)
		{
			// No retry handler found, continue through pipeline
			return await next();
		}

		var circuitBreaker = Policy.Handle<System.Exception>()
			.CircuitBreakerAsync(retryHandler.ExceptionsAllowedBeforeCircuitTrip, System.TimeSpan.FromMilliseconds(5000),
			(exception, things) => { }, () => { });

		var retryPolicy = Policy.Handle<System.Exception>()
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

using Marketplace.Application.Contracts.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Mediator;

public class CustomMediator : IMediator
{
	private readonly IMediator _mediator;

	public CustomMediator(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
	{
		return _mediator.Send(request, cancellationToken);
	}
}
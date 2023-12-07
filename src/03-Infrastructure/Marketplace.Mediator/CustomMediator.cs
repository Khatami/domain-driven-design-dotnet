using Marketplace.Application.Infrastructure.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Mediator;

public class CustomMediator : IMediator
{
	private readonly MediatR.IMediator _mediator;

	public CustomMediator(MediatR.IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
	{
		return _mediator.Send(request!, cancellationToken);
	}
}
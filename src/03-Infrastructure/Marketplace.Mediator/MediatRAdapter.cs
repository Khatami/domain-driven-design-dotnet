using Marketplace.Application.Infrastructure.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Mediator;

public class MediatRAdapter : IApplicationMediator
{
	private readonly MediatR.IMediator _mediator;
    public MediatRAdapter(MediatR.IMediator mediator)
    {
		_mediator = mediator;
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
	{
		var notificationAdapter = new NotificationAdapter<TNotification>(notification);

		return _mediator.Publish(notificationAdapter, cancellationToken);
	}

	public Task Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
	{
		var requestAdapter = new RequestAdapter<TRequest, TResponse>(request);

		return _mediator.Send(requestAdapter, cancellationToken);
	}

	public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
	{
		RequestUnitAdapter<TRequest> requestAdapter = new RequestUnitAdapter<TRequest>(request);

		return _mediator.Send(requestAdapter, cancellationToken);
	}
}
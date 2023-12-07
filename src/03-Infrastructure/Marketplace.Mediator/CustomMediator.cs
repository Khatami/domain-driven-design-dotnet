using Marketplace.Application.Infrastructure.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Mediator;

public class CustomMediator : IMediator
{
	public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
	{
		return Task.CompletedTask;
	}
}
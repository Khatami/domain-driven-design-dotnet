using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class RequestClassifiedAdToPublishCommand : IRequest
{
	public Guid Id { get; set; }
}

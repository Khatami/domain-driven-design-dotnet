using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class CreateClassifiedAdCommand : IRequest
{
	public Guid Id { get; set; }

	public Guid OwnerId { get; set; }
}

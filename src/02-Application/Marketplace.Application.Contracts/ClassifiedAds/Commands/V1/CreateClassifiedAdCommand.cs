using Marketplace.Application.Infrastructure.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class CreateClassifiedAdCommand : ICommand
{
	public Guid Id { get; set; }

	public Guid OwnerId { get; set; }
}

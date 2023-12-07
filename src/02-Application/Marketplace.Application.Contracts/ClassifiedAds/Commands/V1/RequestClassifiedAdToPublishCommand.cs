using Marketplace.Application.Infrastructure.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class RequestClassifiedAdToPublishCommand : ICommand
{
	public Guid Id { get; set; }
}

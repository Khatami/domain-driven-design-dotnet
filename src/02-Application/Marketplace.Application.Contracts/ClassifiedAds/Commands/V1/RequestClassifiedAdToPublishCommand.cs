namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class RequestClassifiedAdToPublishCommand : Mediator.ICommand
{
	public Guid Id { get; set; }
}

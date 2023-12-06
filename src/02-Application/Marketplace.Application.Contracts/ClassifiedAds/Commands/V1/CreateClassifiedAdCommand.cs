namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class CreateClassifiedAdCommand : Mediator.ICommand
{
	public Guid Id { get; set; }

	public Guid OwnerId { get; set; }
}

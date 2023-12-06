namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class UpdateClassifiedAdPriceCommand : Mediator.ICommand
{
	public Guid Id { get; set; }
	public decimal Price { get; set; }
	public string Currency { get; set; }
}

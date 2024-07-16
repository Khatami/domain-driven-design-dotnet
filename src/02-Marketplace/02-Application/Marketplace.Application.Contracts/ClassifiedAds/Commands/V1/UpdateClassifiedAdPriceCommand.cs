using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class UpdateClassifiedAdPriceCommand : ICommand
{
	public Guid Id { get; set; }
	public decimal Price { get; set; }
	public required string Currency { get; set; }
}

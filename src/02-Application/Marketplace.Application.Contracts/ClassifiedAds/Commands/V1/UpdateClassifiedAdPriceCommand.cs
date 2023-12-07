using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class UpdateClassifiedAdPriceCommand : IRequest
{
	public Guid Id { get; set; }
	public decimal Price { get; set; }
	public string Currency { get; set; }
}

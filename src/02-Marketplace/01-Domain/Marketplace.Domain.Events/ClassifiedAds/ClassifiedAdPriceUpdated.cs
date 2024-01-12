namespace Marketplace.Domain.Events.ClassifiedAds
{
	public record ClassifiedAdPriceUpdated(Guid Id, decimal Price, string CurrencyCode);
}

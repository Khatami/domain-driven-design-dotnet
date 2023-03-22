namespace Marketplace.Domain.ClassifiedAds.Events
{
	public record ClassifiedAdPriceUpdated(Guid Id, decimal Price, string CurrencyCode);
}

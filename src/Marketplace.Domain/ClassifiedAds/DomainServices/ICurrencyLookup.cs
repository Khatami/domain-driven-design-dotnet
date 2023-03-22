using Marketplace.Domain.ClassifiedAds.Metadata;

namespace Marketplace.Domain.ClassifiedAds.DomainServices
{
	public interface ICurrencyLookup
	{
		CurrencyDetails FindCurrency(string currencyCode);
	}
}

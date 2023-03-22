using Marketplace.Domain.ClassifiedAd.Metadata;

namespace Marketplace.Domain.ClassifiedAd.DomainServices
{
    public interface ICurrencyLookup
	{
		CurrencyDetails FindCurrency(string currencyCode);
	}
}

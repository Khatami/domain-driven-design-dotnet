using Marketplace.Domain.ClassifiedAds.DomainServices;

namespace Marketplace.Domain.ClassifiedAds.Arguments
{
	public record MoneyArguments(decimal Amount, string Currency, ICurrencyLookup CurrencyLookup);
}

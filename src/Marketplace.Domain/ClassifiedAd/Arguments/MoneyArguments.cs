using Marketplace.Domain.ClassifiedAd.DomainServices;

namespace Marketplace.Domain.ClassifiedAd.Arguments
{
	public record MoneyArguments(decimal Amount, string Currency, ICurrencyLookup CurrencyLookup);
}

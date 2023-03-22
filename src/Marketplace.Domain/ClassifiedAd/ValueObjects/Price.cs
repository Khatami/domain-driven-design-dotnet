using Marketplace.Domain.ClassifiedAd.DomainServices;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record Price : Money
	{
		public Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup) : base(amount, currencyCode, currencyLookup)
		{
			if (amount < 0)
				throw new ArgumentException("Price cannot be negative", nameof(amount));
		}
	}
}

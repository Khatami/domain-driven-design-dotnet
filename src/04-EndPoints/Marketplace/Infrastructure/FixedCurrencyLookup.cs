using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.Metadata;
using Marketplace.Domain.UserProfiles;

namespace Marketplace.Infrastructure
{
	public class FixedCurrencyLookup : ICurrencyLookup
	{
		private static readonly IEnumerable<Currency> _currencies =
			new[]
			{
				new Currency
				{
					CurrencyCode = "EUR",
					DecimalPoints = 2,
					IsUse = true
				},
				new Currency
				{
					CurrencyCode = "USD",
					DecimalPoints = 2,
					IsUse = true
				}
			};

		public Currency FindCurrency(string currencyCode)
		{
			var currency = _currencies.FirstOrDefault(x => x.CurrencyCode == currencyCode);
			return currency ?? Currency.None;
		}
	}
}
